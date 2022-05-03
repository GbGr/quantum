#region

using Photon.Deterministic;

#endregion

namespace Quantum;

partial class RuntimePlayer
{
    public AssetRefEntityPrototype playerPrototype;
    public TeamKey myTeam;

    partial void SerializeUserData(BitStream stream)
    {
        stream.Serialize(ref playerPrototype.Id);

        SerializeTeam(stream);
    }

    private void SerializeTeam(BitStream stream)
    {
        var teamId = (int)myTeam;
        stream.Serialize(ref teamId);
        myTeam = (TeamKey)teamId;
    }
}