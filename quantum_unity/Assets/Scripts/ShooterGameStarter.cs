#region

using System.Collections.Generic;
using Photon.Deterministic;
using Photon.Realtime;
using Quantum;
using UnityEngine;
using Button = UnityEngine.UI.Button;

#endregion

public class ShooterGameStarter : ShooterClientCallbacks, IMatchmakingCallbacks
{
    [SerializeField] private Button gameStartBtn;

    private void Awake()
    {
        gameStartBtn.onClick.AddListener(OnStartGameClicked);
    }

    private void OnStartGameClicked()
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
        
        gameStartBtn.gameObject.SetActive(false);
    }

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
    }

    public void OnCreatedRoom()
    {
        Debug.Log($"{nameof(OnCreatedRoom)}");
        gameStartBtn.gameObject.SetActive(true);
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"{nameof(OnCreateRoomFailed)}");
    }

    public void OnJoinedRoom()
    {
        Debug.Log($"{nameof(OnJoinedRoom)}");
        gameStartBtn.gameObject.SetActive(true);
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
}