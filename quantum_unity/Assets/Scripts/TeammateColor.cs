#region

using System.Collections.Generic;
using Quantum;
using UnityEngine;

#endregion

[RequireComponent(typeof(EntityComponentTeammate))]
public class TeammateColor : EntityViewComponent<Teammate>
{
    [SerializeField] private MeshRenderer meshRenderer;

    private readonly Dictionary<TeamKey, Color> teamColors = new Dictionary<TeamKey, Color>
    {
        { TeamKey.Blue, Color.blue },
        { TeamKey.Red, Color.red }
    };

    private unsafe void Start()
    {
        meshRenderer.material.color = teamColors[QComponent->Team];
    }
}