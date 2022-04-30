using Photon.Deterministic;

namespace Quantum.Game;

public unsafe class VehicleSystem : SystemMainThread
{
    public override void Update(Frame f)
    {
        var vehicles = f.Unsafe.GetComponentBlockIterator<PlayerVehicle>();
        var input = f.GetPlayerInput(0);

        foreach (var vehicle in vehicles)
        {
            var chassisRb = f.Unsafe.GetPointer<PhysicsBody3D>(vehicle.Component->chassis);
            var chassisTransform = f.Unsafe.GetPointer<Transform3D>(vehicle.Component->chassis);
            var wheelTransform = f.Unsafe.GetPointer<Transform3D>(vehicle.Component->wheel_fl);

            if (input != null)
            {
                chassisRb->AddForce(input->Movement.XOY * FP._100 * f.DeltaTime);
            }

            // if (f.RNG->Next(1, 101) >= 100)
            // {
            //     Log.Debug("Force up");
            //     chassisRb->AddForce(new FPVector3(0, 300, 0));
            // }
            
            Log.Debug("Wheel FL Position: " + chassisTransform->Position + wheelTransform->Position);
        }
    }
}