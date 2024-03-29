#region

using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;

#endregion

public class ShooterRoomView : ShooterClientCallbacks, IMatchmakingCallbacks
{
    [SerializeField] private TextMeshProUGUI textField;

    private void UpdateUI()
    {
        if (ShooterClient.Client == null || 
            ShooterClient.Client.CurrentRoom == null)
        {
            textField.gameObject.SetActive(false);
            return;
        }

        var room = ShooterClient.Client.CurrentRoom;
        textField.gameObject.SetActive(true);
        textField.text = "Room: " + room.Name;

        foreach (var kv in room.CustomProperties)
        {
            textField.text += $"\nProp: {kv.Key} - {kv.Value}";
        }
    }

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
    }

    public void OnCreatedRoom()
    {
        UpdateUI();
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
    }

    public void OnJoinedRoom()
    {
        UpdateUI();
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
    }

    public void OnLeftRoom()
    {
        UpdateUI();
    }
}