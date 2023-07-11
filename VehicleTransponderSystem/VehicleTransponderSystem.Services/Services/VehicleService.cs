using VehicleTransponderSystem.Data.Models;

public class VehicleService : IVehicleService
{
	private IVehicleEventHandler _vehicleEventHandler; 

	public VehicleService(IVehicleEventHandler vehicleEventHandler)
	{
		_vehicleEventHandler = vehicleEventHandler; 
	}

	public Vehicle CreateVehicle(Vehicle vehicle)
	{
		// create the vehicle
		_vehicleEventHandler.HandleVehicleCreated(vehicle);

		return vehicle;
	}
}
