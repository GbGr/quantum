// <auto-generated>
// This code was auto-generated by a tool, every time
// the tool executes this code will be reset.
//
// If you need to extend the classes generated to add
// fields or methods to them, please create partial  
// declarations in another file.
// </auto-generated>

using Quantum;
using UnityEngine;

[CreateAssetMenu(menuName = "Quantum/AIAction/MoveToWayPointUsingNavmesh", order = Quantum.EditorDefines.AssetMenuPriorityStart + 12)]
public partial class MoveToWayPointUsingNavmeshAsset : AIActionAsset {
  public MoveToWayPointUsingNavmesh Settings;

  public override Quantum.AssetObject AssetObject => Settings;
  
  public override void Reset() {
    if (Settings == null) {
      Settings = new MoveToWayPointUsingNavmesh();
    }
    base.Reset();
  }
}

public static partial class MoveToWayPointUsingNavmeshAssetExts {
  public static MoveToWayPointUsingNavmeshAsset GetUnityAsset(this MoveToWayPointUsingNavmesh data) {
    return data == null ? null : UnityDB.FindAsset<MoveToWayPointUsingNavmeshAsset>(data);
  }
}