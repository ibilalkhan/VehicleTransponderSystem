using VehicleTransponderSystem.Data.Models;

public interface IVehicleEventHandler
{
	void HandleVehicleCreated(Vehicle vehicle);
}
