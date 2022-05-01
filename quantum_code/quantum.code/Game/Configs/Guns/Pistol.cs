#region

using System;
using Photon.Deterministic;

#endregion

namespace Quantum;

[Serializable]
public class Pistol : GunConfig
{
    public override unsafe void Fire(Frame f, FPVector3 firePosition, FPVector3 fireDirection)
    {
        var ammo = f.Create(Ammo);

        var t = f.Unsafe.GetPointer<Transform3D>(ammo);
        t->Position = firePosition;
        t->Rotation = FPQuaternion.LookRotation(fireDirection);

        var body = f.Unsafe.GetPointer<PhysicsBody3D>(ammo);
        body->AddLinearImpulse(fireDirection * AmmoSpeed);
    }
}