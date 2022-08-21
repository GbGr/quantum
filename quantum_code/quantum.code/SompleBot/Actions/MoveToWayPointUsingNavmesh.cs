using System;
using Photon.Deterministic;
using Quantum;

[Serializable]
public unsafe class MoveToWayPointUsingNavmesh : AIAction
{
    public AIBlackboardValueKey wayPointIndex;

    public override void Update(Frame f, EntityRef entity)
    {
        var bb = f.Unsafe.GetPointer<AIBlackboardComponent>(entity);
        var index = bb->GetInteger(f, wayPointIndex.Key);
        var destination = GetWayPointDestination(f, index);
        var navMeshPathfinder = f.Unsafe.GetPointer<NavMeshPathfinder>(entity);
        var navmesh = f.Map.NavMeshes["QuantumNavmesh"];
        navMeshPathfinder->SetTarget(f, destination, navmesh);
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