using VehicleTransponderSystem.Data.Models;

public interface ITransponderRepository
{
	Transponder CreateTransponder(Guid vehicleId);
}

