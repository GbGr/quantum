using System;
using Quantum;


[Serializable]
public unsafe partial class SetNextWayPoint : AIAction
{
    public AIBlackboardValueKey wayPointIndex;
    
    public override void Update(Frame f, EntityRef entity)
    {
        var numOfWayPoints = f.ComponentCount<WayPoint>();

        var bb = f.Unsafe.GetPointer<AIBlackboardComponent>(entity);
        var currIndex = bb->GetInteger(f, wayPointIndex.Key);

        currIndex++;
        if (currIndex >= numOfWayPoints)
        {
            currIndex = 0;
        }
        
        bb->Set(f, wayPointIndex.Key, currIndex);

        Log.Info($"New index {bb->GetInteger(f, wayPointIndex.Key)}");
    }
}