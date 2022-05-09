using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class ShooterConnector : ShooterClientCallbacks, IConnectionCallbacks
{
    [SerializeField] private Button ConnectBtn;

    private void Awake()
    {
        ConnectBtn.onClick.AddListener(OnConnectBtnClicked);
    }

    private void OnConnectBtnClicked()
    {
        ShooterClient.CreateClientAndConnect();
    }

    public void OnConnected()
    {
        Debug.Log($"{nameof(OnConnected)}");
        
        ConnectBtn.gameObject.SetActive(false);
    }

    public void OnConnectedToMaster()
    {
        Debug.Log($"{nameof(OnConnectedToMaster)}");

        var runtimeConfigSO = RuntimeConfigSO.GetInstance();
        var mapGuid = runtimeConfigSO.Config.Map.Id.Value;
        var customProps = new Hashtable { { "m", mapGuid } };
        var joinRandomParams = new OpJoinRandomRoomParams();

        var enterRoomParams = new EnterRoomParams
        {
            RoomOptions = new RoomOptions
            {
                MaxPlayers = (byte)4,
                CustomRoomProperties = customProps
            }
        };

        Debug.Log("Starting random matchmaking");
        ShooterClient.Client.OpJoinRandomOrCreateRoom(joinRandomParams, enterRoomParams);
        // ShooterClient.Client.OpCreateRoom(enterRoomParams);
    }

    public void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"{nameof(OnDisconnected)}");
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {
        Debug.Log($"{nameof(OnRegionListReceived)}");
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        Debug.Log($"{nameof(OnCustomAuthenticationResponse)}");
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
        Debug.Log($"{nameof(OnCustomAuthenticationFailed)}");
    }
}
