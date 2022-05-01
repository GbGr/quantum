#region

using UnityEngine;

#endregion

[ExecuteInEditMode]
public partial class EntityComponentFirePoint
{
    [Header("Editor:")] 
    [SerializeField] private Transform FirePoint;

    #if UNITY_EDITOR
    private void Update()
    {
        if (Application.isPlaying)
        {
            return;
        }

        if (!FirePoint)
        {
            Prototype.Offset = default;
            return;
        }

        Prototype.Offset = transform.InverseTransformPoint(FirePoint.position).ToFPVector3();
    }
    #endif
}