
using System;
using Photon.Deterministic;
using Quantum;

[Serializable]
public unsafe partial class MoveToWayPoint : AIAction
{
    public AIBlackboardValueKey wayPointIndex;
    public FP speed;
    
    public override void Update(Frame f, EntityRef entity)
    {
        var bb = f.Unsafe.GetPointer<AIBlackboardComponent>(entity);
        var index = bb->GetInteger(f, wayPointIndex.Key);
        
        Log.Info($"Move to index {index}");
        var destination = GetWayPointDestination(f, index);
        var transform = f.Unsafe.GetPointer<Transform3D>(entity);

        transform->Position = FPVector3.MoveTowards(transform->Position, destination, speed);
        transform->Rotation =
            FPQuaternion.SimpleLookAt((destination - transform->Position).Normalized);
    }

    public static FPVector3 GetWayPointDestination(Frame f, int index)
    {
        var wayPointsFilter = f.Filter<WayPointsContainer>();
        wayPointsFilter.NextUnsafe(out var entityRef, out var wayPointsContainer);

        var wayPointsList = f.ResolveList(wayPointsContainer->WayPoints);
        var wayPoint = wayPointsList[index];

        var wayPointTransform = f.Unsafe.GetPointer<Transform3D>(wayPoint);
        return wayPointTransform->Position;
    }
}