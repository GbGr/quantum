﻿using Photon.Deterministic;
using System;
using Quantum.Prototypes;
using Quantum.Collections;

namespace Quantum
{
	[Serializable]
	public struct ResponseCurvePack
	{
		public AssetRefAIFunction ResponseCurveRef;
		[NonSerialized] public ResponseCurve ResponseCurve;
	}

	// ============================================================================================================

	public unsafe partial class Consideration : IConsiderationProvider
	{
		// ========== PUBLIC MEMBERS ==================================================================================

		public string Label;

		public AssetRefAIFunction RankRef;
		public AssetRefAIFunction CommitmentRef;
		public AssetRefConsideration[] NextConsiderationsRefs;
		public AssetRefAIAction[] OnEnterActionsRefs;
		public AssetRefAIAction[] OnUpdateActionsRefs;
		public AssetRefAIAction[] OnExitActionsRefs;

		[NonSerialized] public AIFunction<int> Rank;
		[NonSerialized] public AIFunction<bool> Commitment;
		[NonSerialized] public Consideration[] NextConsiderations;
		[NonSerialized] public AIAction[] OnEnterActions;
		[NonSerialized] public AIAction[] OnUpdateActions;
		[NonSerialized] public AIAction[] OnExitActions;

		public ResponseCurvePack[] ResponseCurvePacks;

		public FP BaseScore;

		public UTMomentumData MomentumData;
		public FP Cooldown;

		public byte Depth;

		// ========== PUBLIC METHODS ==================================================================================

		public AssetRefConsideration GetConsideration(QList<AssetRefConsideration> sourceList, int id)
		{
			return NextConsiderations[id];
		}

		public int Count(QList<AssetRefConsideration> sourceList)
		{
			return NextConsiderations.Length;
		}

		public int GetRank(FrameThreadSafe frame, EntityRef entity = default)
		{
			if (Rank == null)
				return 0;

			return Rank.Execute(frame, entity);
		}

		public FP Score(FrameThreadSafe frame, EntityRef entity = default)
		{
			if (ResponseCurvePacks.Length == 0)
				return 0;

			FP score = 1;
			for (int i = 0; i < ResponseCurvePacks.Length; i++)
			{
				score *= ResponseCurvePacks[i].ResponseCurve.Execute(frame, entity);

				// If we find a negative veto, the final score would be zero anyways, so we stop here
				if (score == 0)
				{
					break;
				}
			}

			score += BaseScore;

			FP modificationFactor = 1 - (1 / ResponseCurvePacks.Length);
			FP makeUpValue = (1 - score) * modificationFactor;
			FP finalScore = score + (makeUpValue * score);

			return finalScore;
		}

		public void OnEnter(FrameThreadSafe frame, UtilityReasoner* reasoner, EntityRef entity = default)
		{
			for (int i = 0; i < OnEnterActions.Length; i++)
			{
				OnEnterActions[i].Update(frame, entity);
			}
		}

		public void OnExit(FrameThreadSafe frame, UtilityReasoner* reasoner, EntityRef entity = default)
		{
			for (int i = 0; i < OnExitActions.Length; i++)
			{
				OnExitActions[i].Update(frame, entity);
			}
		}

		public void OnUpdate(FrameThreadSafe frame, UtilityReasoner* reasoner, EntityRef entity = default)
		{
			for (int i = 0; i < OnUpdateActions.Length; i++)
			{
				OnUpdateActions[i].Update(frame, entity);
			}

			if (NextConsiderationsRefs != null && NextConsiderationsRefs.Length > 0)
			{
				Consideration chosenConsideration = reasoner->SelectBestConsideration(frame, this, (byte)(Depth + 1), reasoner, entity);
				if (chosenConsideration != default)
				{
					chosenConsideration.OnUpdate(frame, reasoner, entity);
					UTManager.ConsiderationChosen?.Invoke(entity, chosenConsideration.Identifier.Guid.Value);
				}
			}
		}

		public override void Loaded(IResourceManager resourceManager, Native.Allocator allocator)
		{
			base.Loaded(resourceManager, allocator);

			Rank = (AIFunction<int>)resourceManager.GetAsset(RankRef.Id);

			if (ResponseCurvePacks != null)
			{
				for (Int32 i = 0; i < ResponseCurvePacks.Length; i++)
				{
					ResponseCurvePacks[i].ResponseCurve = (ResponseCurve)resourceManager.GetAsset(ResponseCurvePacks[i].ResponseCurveRef.Id);
				}
			}

			OnEnterActions = new AIAction[OnEnterActionsRefs == null ? 0 : OnEnterActionsRefs.Length];
			if (OnEnterActionsRefs != null)
			{
				for (Int32 i = 0; i < OnEnterActionsRefs.Length; i++)
				{
					OnEnterActions[i] = (AIAction)resourceManager.GetAsset(OnEnterActionsRefs[i].Id);
				}
			}

			OnUpdateActions = new AIAction[OnUpdateActionsRefs == null ? 0 : OnUpdateActionsRefs.Length];
			if (OnUpdateActionsRefs != null)
			{
				for (Int32 i = 0; i < OnUpdateActionsRefs.Length; i++)
				{
					OnUpdateActions[i] = (AIAction)resourceManager.GetAsset(OnUpdateActionsRefs[i].Id);
				}
			}

			OnExitActions = new AIAction[OnExitActionsRefs == null ? 0 : OnExitActionsRefs.Length];
			if (OnExitActionsRefs != null)
			{
				for (Int32 i = 0; i < OnExitActionsRefs.Length; i++)
				{
					OnExitActions[i] = (AIAction)resourceManager.GetAsset(OnExitActionsRefs[i].Id);
				}
			}

			Commitment = (AIFunction<bool>)resourceManager.GetAsset(CommitmentRef.Id);

			NextConsiderations = new Consideration[NextConsiderationsRefs == null ? 0 : NextConsiderationsRefs.Length];
			if (NextConsiderationsRefs != null)
			{
				for (Int32 i = 0; i < NextConsiderationsRefs.Length; i++)
				{
					NextConsiderations[i] = (Consideration)resourceManager.GetAsset(NextConsiderationsRefs[i].Id);
				}
			}
		}
	}
}
