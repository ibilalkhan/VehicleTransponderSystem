// VehicleTransponderSystem.Data.Models -> Transponder.cs
using System;

namespace VehicleTransponderSystem.Data.Models
{
	public class Transponder
	{
		public Guid Id { get; set; }
		public Guid VehicleId { get; set; }
	}
}
