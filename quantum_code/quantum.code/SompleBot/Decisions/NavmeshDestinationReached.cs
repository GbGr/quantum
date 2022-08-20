using System;
using System.Diagnostics;
using Quantum;

[Serializable]
public unsafe partial class NavmeshDestinationReached : HFSMDecision
{
    public override bool Decide(Frame f, EntityRef entity)
    {
        var pathfinder = f.Unsafe.GetPointer<NavMeshPathfinder>(entity);
        return true;
    }
}