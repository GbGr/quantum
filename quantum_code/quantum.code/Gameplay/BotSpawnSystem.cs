namespace Quantum.Gameplay;

public class BotSpawnSystem : SystemSignalsOnly
{
    private const string BOT_PROTOTYPE_PATH = 
        "Resources/DB/Prefabs/EnemyBot|EntityPrototype";
    
    public override unsafe void OnInit(Frame f)
    {
        var botPrototype = f.FindAsset<EntityPrototype>(BOT_PROTOTYPE_PATH);
        var botEntity = f.Create(botPrototype);
        
        var filter = f.Filter<SpawnPoint, Transform3D>();
        filter.Next(out _, out _, out var spawnTransform);
        
        var botTransform = f.Unsafe.GetPointer<Transform3D>(botEntity);
        botTransform->Position = spawnTransform.Position;
    }
}