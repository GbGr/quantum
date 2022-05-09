#region

using System.Collections.Generic;
using UnityEngine;

#endregion

public class ShooterClient : MonoBehaviour
{
    public static QuantumLoadBalancingClient Client { get; private set; }

    private static readonly List<object> listenersBuffer = new List<object>();

    public static void CreateClientAndConnect()
    {
        var appSettings = PhotonServerSettings.CloneAppSettings(PhotonServerSettings.Instance.AppSettings);
        Client = new QuantumLoadBalancingClient(appSettings.Protocol);

        SubscribeFromBuffer();

        Client.ConnectUsingSettings(appSettings);
    }

    private static void SubscribeFromBuffer()
    {
        foreach (var listener in listenersBuffer)
        {
            Client.AddCallbackTarget(listener);
        }

        listenersBuffer.Clear();
    }

    public static void AddListener(object callbackObject)
    {
        if (Client != null)
        {
            Client.AddCallbackTarget(callbackObject);
            return;
        }

        listenersBuffer.Add(callbackObject);
    }

    public static void RemoveListener(object callbackObject)
    {
        if (Client != null)
        {
            Client.RemoveCallbackTarget(callbackObject);
            return;
        }

        listenersBuffer.Remove(callbackObject);
    }

    // Super important piece if fucking code!!!
    private void Update()
    {
        Client?.Service();
    }
}