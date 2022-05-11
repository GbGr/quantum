#region

using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Deterministic;
using Photon.Realtime;
using Quantum;
using UnityEngine;
using Button = UnityEngine.UI.Button;

#endregion

public class ShooterGameStarter : ShooterClientCallbacks, 
                                  IMatchmakingCallbacks,
                                  IOnEventCallback
{
    [SerializeField] private Button gameStartBtn;

    private void Awake()
    {
        gameStartBtn.onClick.AddListener(OnStartGameClicked);
    }

    private void OnStartGameClicked()
    {
        gameStartBtn.gameObject.SetActive(false);

        var options = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        var code = ShooterEventCode.StartGame;
        ShooterClient.Client.OpRaiseEvent((byte)code, null, options, SendOptions.SendReliable);
    }

    private void StartGame()
    {
        var config = RuntimeConfigSO.GetInstance().Config;
        var param = new QuantumRunner.StartParameters
        {
            RuntimeConfig = config,
            DeterministicConfig = DeterministicSessionConfigAsset.Instance.Config,
            ReplayProvider = null,
            GameMode = DeterministicGameMode.Multiplayer,
            FrameData = null,
            InitialFrame = 0,
            PlayerCount = 4,
            LocalPlayerCount = 1,
            RecordingFlags = RecordingFlags.None,
            NetworkClient = ShooterClient.Client,
            StartGameTimeoutInSeconds = 10.0f
        };

        QuantumRunner.StartGame("IgorTime" + Random.value, param);
    }

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
    }

    public void OnCreatedRoom()
    {
        Debug.Log($"{nameof(OnCreatedRoom)}");

        EnableStartGameBtnIfMaster();
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"{nameof(OnCreateRoomFailed)}");
    }

    public void OnJoinedRoom()
    {
        Debug.Log($"{nameof(OnJoinedRoom)}");

        EnableStartGameBtnIfMaster();
    }

    private void EnableStartGameBtnIfMaster()
    {
        var isMaster = ShooterClient.Client.LocalPlayer.IsMasterClient;
        gameStartBtn.gameObject.SetActive(isMaster);
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"{nameof(OnJoinRoomFailed)}");
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"{nameof(OnJoinRandomFailed)}");
    }

    public void OnLeftRoom()
    {
        Debug.Log($"{nameof(OnLeftRoom)}");
    }

    public void OnEvent(EventData photonEvent)
    {
        var code = (ShooterEventCode)photonEvent.Code;
        switch (code)
        {
            case ShooterEventCode.StartGame:
                StartGame();
                break;
        }
    }
}