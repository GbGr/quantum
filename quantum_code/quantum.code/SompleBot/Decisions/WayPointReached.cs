using System;
using Photon.Deterministic;
using Quantum;


[Serializable]
public unsafe partial class WayPointReached : HFSMDecision
{
    public AIBlackboardValueKey CurrentWayPointIndex;
    public FP Radius;
    
    public override bool Decide(Frame f, EntityRef entity)
    {
        var t = f.Unsafe.GetPointer<Transform3D>(entity);
        var pos = t->Position;

        var bb = f.Unsafe.GetPointer<AIBlackboardComponent>(entity);
        var index = bb->GetInteger(f, CurrentWayPointIndex.Key);
        
        var destination = MoveToWayPoint.GetWayPointDestination(f, index);
        var distSqr = FPVector3.DistanceSquared(pos, destination);

        return distSqr <= Radius * Radius;
    }
}