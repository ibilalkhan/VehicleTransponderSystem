using VehicleTransponderSystem.Data.Models;

public class VehicleEventHandler : IVehicleEventHandler
{
	private readonly ITransponderFactory _transponderFactory;

	public VehicleEventHandler(ITransponderFactory transponderFactory)
	{
		_transponderFactory = transponderFactory;
	}

	public void HandleVehicleCreated(Vehicle vehicle)
	{
		var transponderRepository = _transponderFactory.Create(vehicle.Year);
		var transponder = transponderRepository.CreateTransponder(vehicle.Id);

		// Associate transponder with the vehicle
		vehicle.Transponder = transponder;
	}
}
