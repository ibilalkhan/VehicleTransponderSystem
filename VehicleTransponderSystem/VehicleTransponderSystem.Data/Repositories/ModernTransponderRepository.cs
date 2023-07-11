using VehicleTransponderSystem.Data.Models;

public class ModernTransponderRepository : ITransponderRepository
{
	public Transponder CreateTransponder(Guid vehicleId)
	{
		Console.WriteLine($"Modern Transponder Created with ID: {vehicleId}");

		return new Transponder { VehicleId = vehicleId };
	}
}
