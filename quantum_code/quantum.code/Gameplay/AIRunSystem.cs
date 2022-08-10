namespace Quantum.Gameplay;

public unsafe class AIRunSystem : SystemMainThread, ISignalOnComponentAdded<HFSMAgent>
{
    public void OnAdded(Frame f, EntityRef entity, HFSMAgent* component)
    {
        var hfsmRoot = f.FindAsset<HFSMRoot>(component->Data.Root.Id);
        HFSMManager.Init(f, entity, hfsmRoot);
    }

    public override void Update(Frame f)
    {
        var allAgents = f.Filter<HFSMAgent>();
        while (allAgents.NextUnsafe(out var entity, out _))
        {
            HFSMManager.Update(f, f.DeltaTime, entity);
        }
    }
}