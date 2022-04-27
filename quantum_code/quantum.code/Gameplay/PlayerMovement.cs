using Photon.Deterministic;

namespace Quantum.Gameplay;

public unsafe class PlayerMovement: SystemMainThreadFilter<PlayerMovement.Filter>
{
    public struct Filter
    {
        public EntityRef EntityRef;
        public PlayerLink* PlayerLink;
        public Transform3D* Transform;
        public CharacterController3D* Controller3D;
    }

    public override void Update(Frame f, ref Filter filter)
    {
        var input = f.GetPlayerInput(filter.PlayerLink->PlayerRef);

        var movement = input->Movement;
        if (movement.SqrMagnitude > 1)
        {
            movement = movement.Normalized;
        }
        
        filter.Controller3D->Move(f, filter.EntityRef, movement.XOY);

        if (movement != default)
        {
            filter.Transform->Rotation = FPQuaternion.LookRotation(movement.XOY);
        }
    }
}