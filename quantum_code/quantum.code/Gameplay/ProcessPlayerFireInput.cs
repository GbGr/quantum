namespace Quantum.Gameplay;

public unsafe class ProcessPlayerFireInput : SystemMainThreadFilter<ProcessPlayerFireInput.Filter>
{
    public struct  Filter
    {
        public EntityRef EntityRef;
        public PlayerLink* PlayerLink;
        public Shooting* Shooting;
    }

    public override void Update(Frame f, ref Filter filter)
    {
        var playerInput = f.GetPlayerInput(filter.PlayerLink->PlayerRef);
        filter.Shooting->IsShooting = playerInput->Fire.IsDown;
    }
}