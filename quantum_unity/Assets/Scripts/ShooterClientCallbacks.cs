#region

using UnityEngine;

#endregion

public class ShooterClientCallbacks : MonoBehaviour
{
    private void OnEnable()
    {
        ShooterClient.AddListener(this);
    }

    private void OnDisable()
    {
        ShooterClient.RemoveListener(this);
    }
}