#region

using Quantum;
using UnityEngine;

#endregion

[CreateAssetMenu]
public class RuntimeConfigSO : ScriptableObject
{
    public RuntimeConfig Config;

    public static RuntimeConfigSO GetInstance()
    {
        return Resources.Load<RuntimeConfigSO>(nameof(RuntimeConfigSO));
    }
}