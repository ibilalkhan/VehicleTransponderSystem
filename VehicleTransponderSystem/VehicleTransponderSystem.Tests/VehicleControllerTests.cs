using Microsoft.AspNetCore.Mvc;
using Moq;
using VehicleTransponderSystem.Data.Models;

public class VehicleControllerTests
{
	private readonly Mock<IVehicleService> _mockVehicleService;
	private readonly VehicleController _vehicleController;

	public VehicleControllerTests()
	{
		_mockVehicleService = new Mock<IVehicleService>();
		_vehicleController = new VehicleController(_mockVehicleService.Object);
	}

	private Vehicle CreateTestVehicle()
	{
		return new Vehicle();
	}

	[Fact]
	public void Should_ReturnBadRequest_When_PostingNullVehicle()
	{
		// Act
		var result = _vehicleController.Post(null).Result;

		// Assert
		Assert.IsType<BadRequestObjectResult>(result);
	}

	[Fact]
	public void Should_ReturnBadRequest_When_PostingVehicleWithInvalidModelState()
	{
		// Arrange
		_vehicleController.ModelState.AddModelError("Error", "Invalid model");

		// Act
		var result = _vehicleController.Post(new Vehicle()).Result;

		// Assert
		Assert.IsType<BadRequestObjectResult>(result);
	}

	[Fact]
	public void Should_CallCreateVehicleOnce_When_PostingValidVehicle()
	{
		// Arrange
		var vehicle = CreateTestVehicle();

		// Act
		_vehicleController.Post(vehicle);

		// Assert
		_mockVehicleService.Verify(service => service.CreateVehicle(vehicle), Times.Once);
	}

	[Fact]
	public void Should_ReturnInternalError_When_CreateVehicleFails()
	{
		// Arrange
		var vehicle = CreateTestVehicle();
		_mockVehicleService.Setup(service => service.CreateVehicle(vehicle)).Returns(() => null);

		// Act
		var result = _vehicleController.Post(vehicle).Result;

		// Assert
		Assert.IsType<ObjectResult>(result);
		Assert.Equal(500, ((ObjectResult)result).StatusCode);
	}

	[Fact]
	public void Should_ReturnOkAndCreatedVehicle_When_CreateVehicleSucceeds()
	{
		// Arrange
		var vehicle = CreateTestVehicle();
		_mockVehicleService.Setup(service => service.CreateVehicle(vehicle)).Returns(vehicle);

		// Act
		var result = _vehicleController.Post(vehicle).Result;

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var createdVehicle = Assert.IsType<Vehicle>(okResult.Value);
		Assert.Same(vehicle, createdVehicle);
	}
}
