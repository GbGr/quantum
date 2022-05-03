#region

using System.Collections.Generic;
using Quantum;
using UnityEngine;

#endregion

public class TeammateColor : MonoBehaviour
{
    [SerializeField] private EntityView myView;
    [SerializeField] private MeshRenderer meshRenderer;

    private readonly Dictionary<TeamKey, Color> teamColors = new Dictionary<TeamKey, Color>
    {
        { TeamKey.Blue, Color.blue },
        { TeamKey.Red, Color.red }
    };

    private unsafe void Start()
    {
        var f = QuantumRunner.Default.Game.Frames.Predicted;
        var teammate = f.Unsafe.GetPointer<Teammate>(myView.EntityRef);

        meshRenderer.material.color = teamColors[teammate->Team];
    }
}