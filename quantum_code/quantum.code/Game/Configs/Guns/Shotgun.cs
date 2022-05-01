#region

using System;
using Photon.Deterministic;

#endregion

namespace Quantum;

[Serializable]
public class Shotgun : GunConfig
{
    public FP BulletAmount = 5;
    public FP Angle = 60;

    public override void Fire(Frame f, FPVector3 shootPoint, FPVector3 direction)
    {
        var bulletsToSpawn = BulletAmount;
        var startAngle = Angle / 2;
        var angleStep = Angle / (bulletsToSpawn - 1);

        var isEven = BulletAmount % 2 == 0;
        if (!isEven)
        {
            SpawnBullet(f, shootPoint, direction);
            bulletsToSpawn -= 1;
        }

        for (var i = 0; i < bulletsToSpawn / 2; i++)
        {
            var dir1 = FPQuaternion.Euler(0, startAngle, 0) * direction;
            var dir2 = FPQuaternion.Euler(0, startAngle * -1, 0) * direction;
            SpawnBullet(f, shootPoint, dir1);
            SpawnBullet(f, shootPoint, dir2);

            startAngle -= angleStep;
        }
    }
    
    private unsafe void SpawnBullet(Frame f, FPVector3 shootPoint, FPVector3 direction)
    {
        var ammo = f.Create(Ammo);

        var t = f.Unsafe.GetPointer<Transform3D>(ammo);
        t->Position = shootPoint;
        t->Rotation = FPQuaternion.LookRotation(direction);

        var body = f.Unsafe.GetPointer<PhysicsBody3D>(ammo);
        body->AddLinearImpulse(direction * AmmoSpeed);
    }
}