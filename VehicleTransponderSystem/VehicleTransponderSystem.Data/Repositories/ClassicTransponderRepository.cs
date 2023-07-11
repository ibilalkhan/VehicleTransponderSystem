using VehicleTransponderSystem.Data.Models;

public class ClassicTransponderRepository : ITransponderRepository
{
	public Transponder CreateTransponder(Guid vehicleId)
	{
		Console.WriteLine($"Classic Transponder Created with ID: {vehicleId}");

		return new Transponder { VehicleId = vehicleId };
	}
}