using Microsoft.AspNetCore.Mvc;
using VehicleTransponderSystem.Data.Models;
using System;

[ApiController]
[Route("[controller]")]
public class VehicleController : ControllerBase
{
	private readonly IVehicleService _vehicleService;

	public VehicleController(IVehicleService vehicleService)
	{
		_vehicleService = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));
	}

	[HttpPost]
	public ActionResult<Vehicle> Post([FromBody] Vehicle vehicle)
	{
		if (vehicle == null)
		{
			return BadRequest("Vehicle information is null. Please provide valid vehicle information.");
		}

		// Validate the vehicle model data. If it's not valid, return a BadRequest response
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		try
		{
			var createdVehicle = _vehicleService.CreateVehicle(vehicle);

			// If the vehicle could not be created, return a 500 Internal Server Error response
			if (createdVehicle == null)
			{
				return StatusCode(500, "A problem happened while handling your request.");
			}

			return Ok(createdVehicle);
		}

		// Catch any exceptions that occur during vehicle creation and return a 500 Internal Server Error response
		catch (Exception ex)
		{ 
			return StatusCode(500, $"A problem happened while handling your request: {ex.Message}");
		}
	}
}
