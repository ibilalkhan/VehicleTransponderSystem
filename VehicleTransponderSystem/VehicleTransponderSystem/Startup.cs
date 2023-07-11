using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
 
public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddTransient<ClassicTransponderRepository>();
			services.AddTransient<ModernTransponderRepository>();
			services.AddTransient<ITransponderFactory, TransponderFactory>();
			services.AddTransient<IVehicleService, VehicleService>();
		services.AddScoped<IVehicleEventHandler, VehicleEventHandler>();

		services.AddControllers();
		 
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vehicle Transponder System", Version = "v1" });
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization(); 
			app.UseSwagger(); 
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vehicle Transponder System API V1");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers(); 
			});
		}
	} 