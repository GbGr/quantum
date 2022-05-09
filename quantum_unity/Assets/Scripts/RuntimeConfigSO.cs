#region

using Quantum;
using UnityEngine;

#endregion

[CreateAssetMenu]
public class RuntimeConfigSO : ScriptableObject
{
    public RuntimeConfig Config;

    private static RuntimeConfigSO instance;

    public static RuntimeConfigSO GetInstance()
    {
        if (instance == null)
        {
            var allRuntimeConfigs = Resources.FindObjectsOfTypeAll<RuntimeConfigSO>();
            instance = allRuntimeConfigs.Length == 0 ? null : allRuntimeConfigs[0];
        }
        
        return instance;
    }
}