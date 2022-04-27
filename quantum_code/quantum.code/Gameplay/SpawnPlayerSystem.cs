using Photon.Deterministic;

namespace Quantum.Gameplay;

public class SpawnPlayerSystem : SystemSignalsOnly, ISignalOnPlayerConnected
{
    public void OnPlayerConnected(Frame f, PlayerRef player)
    {
        var filter = f.Filter<SpawnPoint, Transform3D>();
        while (filter.Next( out var entityRef, out var spawnPoint, out var transform))
        {
            if (spawnPoint.Index == player)
            {
                SpawnPlayer(f, transform.Position, player);
            }
        }
    }

    private unsafe void SpawnPlayer(Frame f, FPVector3 position, PlayerRef player)
    {
        var playerData = f.GetPlayerData(player);
        var playerEntity = f.Create(playerData.playerPrototype);

        var transform = f.Unsafe.GetPointer<Transform3D>(playerEntity);
        transform->Position = position;

        var playerLink = f.Unsafe.GetPointer<PlayerLink>(playerEntity);
        playerLink->PlayerRef = player;
    }
}