#region

using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Realtime;
using TMPro;
using UnityEngine;

#endregion

public class ShooterPlayersView : ShooterClientCallbacks,
                                  IInRoomCallbacks,
                                  IMatchmakingCallbacks
{
    [SerializeField] private TextMeshProUGUI textField;

    public void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Player Entered: {newPlayer}");
        UpdateUI();
    }

    public void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"Player left: {otherPlayer}");
        UpdateUI();
    }

    public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
    }

    public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
    }

    public void OnMasterClientSwitched(Player newMasterClient)
    {
    }

    private void UpdateUI()
    {
        var client = ShooterClient.Client;
        var room = client.CurrentRoom;
        var str = string.Empty;
        foreach (var playerKV in room.Players)
        {
            str += $"Player ({playerKV.Key}): {playerKV.Value.NickName} - {playerKV.Value.UserId} \n";
        }

        textField.enabled = !string.IsNullOrEmpty(str);
        textField.text = str;
    }

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
    }

    public void OnCreatedRoom()
    {
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
    }
}