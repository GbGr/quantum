#region

using Quantum;
using UnityEngine;

#endregion

[RequireComponent(typeof(EntityView))]
public abstract unsafe class EntityViewComponent<T> : MonoBehaviour
    where T : unmanaged, IComponent
{
    [SerializeField] private EntityView myView;

    public EntityView MyView => myView;
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
        if (!myView)
        {
            myView = GetComponent<EntityView>();
        }
        
        MyGame = QuantumRunner.Default.Game;
    }

    private void OnValidate()
    {
        myView = GetComponent<EntityView>();
    }
}