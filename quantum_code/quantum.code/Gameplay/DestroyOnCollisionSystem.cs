namespace Quantum.Gameplay;

public class DestroyOnCollisionSystem : SystemSignalsOnly, ISignalOnCollisionEnter3D
{
    public void OnCollisionEnter3D(Frame f, CollisionInfo3D info)
    {
        TryDestroyEntity(f, info.Entity);
        TryDestroyEntity(f, info.Other);
    }

    private unsafe void TryDestroyEntity(Frame f, EntityRef entity)
    {
        var hasComponent = f.Unsafe.TryGetPointer(entity, out DestroyOnCollision* component);
        if (hasComponent)
        {
            f.Destroy(entity);
        }
    }
}