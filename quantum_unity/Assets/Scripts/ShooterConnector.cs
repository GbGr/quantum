using System.Collections.Generic;
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
    }

    public void OnConnectedToMaster()
    {
        Debug.Log($"{nameof(OnConnectedToMaster)}");
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
