using Quantum;
using UnityEngine;

public class LocalPlayerDataSetter : MonoBehaviour
{
    [SerializeField] private RuntimePlayer runtimePlayer;
    
    private void Awake()
    {
        QuantumCallback.SubscribeManual<CallbackGameStarted>(OnGameStarted);
    }

    private void OnGameStarted(CallbackGameStarted callback)
    {
        var game = callback.Game;
        foreach (var player in game.GetLocalPlayers())
        {
            game.SendPlayerData(player, runtimePlayer);
        }
    }
}
