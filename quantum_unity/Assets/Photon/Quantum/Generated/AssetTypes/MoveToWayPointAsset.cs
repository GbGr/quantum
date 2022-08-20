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

[CreateAssetMenu(menuName = "Quantum/AIAction/MoveToWayPoint", order = Quantum.EditorDefines.AssetMenuPriorityStart + 12)]
public partial class MoveToWayPointAsset : AIActionAsset {
  public MoveToWayPoint Settings;

  public override Quantum.AssetObject AssetObject => Settings;
  
  public override void Reset() {
    if (Settings == null) {
      Settings = new MoveToWayPoint();
    }
    base.Reset();
  }
}

public static partial class MoveToWayPointAssetExts {
  public static MoveToWayPointAsset GetUnityAsset(this MoveToWayPoint data) {
    return data == null ? null : UnityDB.FindAsset<MoveToWayPointAsset>(data);
  }
}