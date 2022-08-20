using System;
using Photon.Deterministic;

namespace Quantum
{
	[Serializable]
	[AssetObjectConfig(GenerateLinkingScripts = true, GenerateAssetCreateMenu = false, GenerateAssetResetMethod = false)]
	public partial class TimerDecision : HFSMDecision
	{
		public AIParamFP TimeToTrueState = FP._3;

		public override unsafe bool Decide(Frame f, EntityRef entity)
		{
			var blackboard = f.Has<AIBlackboardComponent>(entity) ? f.Get<AIBlackboardComponent>(entity) : default;

			var agent = f.Unsafe.GetPointer<HFSMAgent>(entity);
			var aiConfig = agent->GetConfig(f);

			FP requiredTime = TimeToTrueState.Resolve(f, entity, &blackboard, aiConfig);

			var hfsmData = &agent->Data;
			return hfsmData->Time >= requiredTime;
		}
	}
}
