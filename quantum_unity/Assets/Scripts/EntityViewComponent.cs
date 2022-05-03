#region

using Quantum;
using UnityEngine;

#endregion

[RequireComponent(typeof(EntityView))]
public abstract unsafe class EntityViewComponent<T> : MonoBehaviour
    where T : unmanaged, IComponent
{
    public EntityView MyView { get; private set; }
    public QuantumGame MyGame { get; private set; }

    public EntityRef MyRef => MyView.EntityRef;

    public T* QComponent
    {
        get
        {
            var f = MyGame.Frames.Predicted;
            var hasComponent = f.Unsafe.TryGetPointer(MyRef, out T* qComponent);
            return hasComponent ? qComponent : default;
        }
    }

    private void Awake()
    {
        MyView = GetComponent<EntityView>();
        MyGame = QuantumRunner.Default.Game;
    }
}