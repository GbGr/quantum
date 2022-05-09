#region

using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

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
        // START GAME
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