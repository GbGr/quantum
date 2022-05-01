using Photon.Deterministic;

namespace Quantum.Gameplay;

public unsafe class ShootingSystem : SystemMainThreadFilter<ShootingSystem.Filter>
{
    public struct Filter
    {
        public EntityRef EntityRef;
        public Shooting* Shooting;
        public FirePoint* FirePoint;
        public Transform3D* Transform;
    }

    public override void Update(Frame f, ref Filter filter)
    {
        filter.Shooting->Cooldown = FPMath.Max(0, filter.Shooting->Cooldown - f.DeltaTime);
        if (filter.Shooting->Cooldown > 0)
        {
            return;
        }

        if (!filter.Shooting->IsShooting)
        {
            return;
        }

        var firePoint = filter.Transform->LocalToWorldMatrix.MultiplyPoint(filter.FirePoint->Offset);
        var fireDirection = filter.Transform->Forward;

        var gun = f.FindAsset<GunConfig>(filter.Shooting->GunConfig.Id);
        gun.Fire(f,firePoint, fireDirection);

        filter.Shooting->Cooldown = gun.FireCooldown;
    }
}