using Photon.Deterministic;
using Quantum;

public unsafe class CustomNavmeshMovement : SystemSignalsOnly, ISignalOnNavMeshMoveAgent
{
    public void OnNavMeshMoveAgent(Frame f, EntityRef entity, FPVector2 desiredDirection)
    {
        var pathfinder = f.Unsafe.GetPointer<NavMeshPathfinder>(entity);
        var transform = f.Unsafe.GetPointer<Transform3D>(entity);

        var currentWayPointIndex = pathfinder->WaypointIndex;
        var wayPoint = pathfinder->GetWaypoint(f, currentWayPointIndex);

        var moveDirection = (wayPoint - transform->Position).Normalized;
        transform->Position += moveDirection * 2 * f.DeltaTime; 
        
        transform->Rotation = FPQuaternion.LookRotation(desiredDirection.XOY, FPVector3.Up);
    }
}