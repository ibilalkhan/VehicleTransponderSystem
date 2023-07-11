using Moq;
using VehicleTransponderSystem.Data.Models;

public class VehicleServiceTests
{
	private Mock<ITransponderFactory> _mockTransponderFactory;
	private Mock<ITransponderRepository> _mockClassicRepository;
	private Mock<ITransponderRepository> _mockModernRepository;
	private Mock<IVehicleEventHandler> _mockVehicleEventHandler;
	private IVehicleService _vehicleService;

	public VehicleServiceTests()
	{
		_mockVehicleEventHandler = new Mock<IVehicleEventHandler>();
		_mockTransponderFactory = new Mock<ITransponderFactory>();
		_mockClassicRepository = new Mock<ITransponderRepository>();
		_mockModernRepository = new Mock<ITransponderRepository>();
		_vehicleService = new VehicleService(_mockVehicleEventHandler.Object);

		_mockTransponderFactory.Setup(f => f.Create(It.IsAny<int>()))
				.Returns<int>(year => year <= DateTime.Now.Year - 25 ? _mockClassicRepository.Object : _mockModernRepository.Object);

		_mockVehicleEventHandler.Setup(v => v.HandleVehicleCreated(It.IsAny<Vehicle>()))
				.Callback<Vehicle>(vehicle =>
					_mockTransponderFactory.Object.Create(vehicle.Year).CreateTransponder(vehicle.Id));
	}

	[Fact]
	public void Should_UseClassicRepository_When_VehicleIsOlderThan25Years()
	{
		// Arrange
		var vehicle = new Vehicle { Year = DateTime.Now.Year - 26 };

		// Act
		_vehicleService.CreateVehicle(vehicle);

		// Assert
		_mockClassicRepository.Verify(r => r.CreateTransponder(vehicle.Id), Times.Once);
		_mockModernRepository.Verify(r => r.CreateTransponder(vehicle.Id), Times.Never);
	}

	[Fact]
	public void Should_UseModernRepository_When_VehicleIsLessThan25YearsOld()
	{
		// Arrange
		var vehicle = new Vehicle { Year = DateTime.Now.Year - 24 };

		// Act
		_vehicleService.CreateVehicle(vehicle);

		// Assert
		_mockClassicRepository.Verify(r => r.CreateTransponder(vehicle.Id), Times.Never);
		_mockModernRepository.Verify(r => r.CreateTransponder(vehicle.Id), Times.Once);
	}  
}
