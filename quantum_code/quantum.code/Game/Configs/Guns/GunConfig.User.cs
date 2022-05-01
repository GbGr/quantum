using Photon.Deterministic;

namespace Quantum
{
    public abstract partial class GunConfig
    {
        public AssetRefEntityPrototype Ammo;
        public FP FireCooldown = FP._1;
        public FP AmmoSpeed = 10;

        public unsafe abstract void Fire(Frame f, FPVector3 firePosition, FPVector3 fireDirection);
    }
}
