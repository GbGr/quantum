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

[CreateAssetMenu(menuName = "Quantum/BTNode/BTDecorator/BTForceResult", order = Quantum.EditorDefines.AssetMenuPriorityStart + 27)]
public partial class BTForceResultAsset : BTDecoratorAsset {
  public Quantum.BTForceResult Settings;

  public override Quantum.AssetObject AssetObject => Settings;
  
  public override void Reset() {
    if (Settings == null) {
      Settings = new Quantum.BTForceResult();
    }
    base.Reset();
  }
}

public static partial class BTForceResultAssetExts {
  public static BTForceResultAsset GetUnityAsset(this BTForceResult data) {
    return data == null ? null : UnityDB.FindAsset<BTForceResultAsset>(data);
  }
}
