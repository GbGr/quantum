#region

using Lean.Pool;
using UnityEngine;

#endregion

public class ShooterEntityViewUpdater : EntityViewUpdater
{
    protected override EntityView CreateEntityViewInstance(
        EntityViewAsset asset,
        Vector3? position = null,
        Quaternion? rotation = null)
    {
        var isPoollable = asset.View.TryGetComponent(out Poollable _);
        if (isPoollable)
        {
            return CreateFromPool(asset, position, rotation);
        }

        return base.CreateEntityViewInstance(asset, position, rotation);
    }

    private EntityView CreateFromPool(EntityViewAsset asset, Vector3? position, Quaternion? rotation)
    {
        Debug.Assert(asset.View != null);

        var view = position.HasValue && rotation.HasValue
            ? LeanPool.Spawn(asset.View, position.Value, rotation.Value)
            : LeanPool.Spawn(asset.View);

        return view;
    }

    protected override void DestroyEntityViewInstance(EntityView instance)
    {
        var isPoollable = instance.TryGetComponent(out Poollable _);
        if (isPoollable)
        {
            DestroyFromPool(instance);
            return;
        }

        base.DestroyEntityViewInstance(instance);
    }

    private void DestroyFromPool(EntityView instance)
    {
        LeanPool.Despawn(instance);
    }
}