using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace VehicleTransponderSystem.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
				Host.CreateDefaultBuilder(args)
						.ConfigureWebHostDefaults(webBuilder =>
						{
							webBuilder.UseStartup<Startup>();
						})
						.ConfigureServices(services =>
						{
							services.AddScoped<ITransponderRepository, ClassicTransponderRepository>();
							services.AddScoped<ITransponderRepository, ModernTransponderRepository>();
							services.AddScoped<IVehicleService, VehicleService>();
							services.AddScoped<ITransponderFactory, TransponderFactory>();
							services.AddScoped<IVehicleEventHandler, VehicleEventHandler>();

						});
	}
}
