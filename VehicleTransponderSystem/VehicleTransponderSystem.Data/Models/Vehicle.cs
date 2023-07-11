// VehicleTransponderSystem.Data.Models -> Vehicle.cs
using System;

namespace VehicleTransponderSystem.Data.Models
{
	public class Vehicle
	{
		public Guid Id { get; set; }
		public string Make { get; set; }
		public string Model { get; set; }
		public int Year { get; set; }
		public Transponder Transponder { get; set; }  // Add this line
	}

}
