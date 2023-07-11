using Microsoft.Extensions.DependencyInjection;
public class TransponderFactory : ITransponderFactory
{
	private readonly IServiceProvider _serviceProvider;

	public TransponderFactory(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public ITransponderRepository Create(int vehicleYear)
	{
		return vehicleYear <= DateTime.Now.Year - 25
				? _serviceProvider.GetRequiredService<ClassicTransponderRepository>()
				: _serviceProvider.GetRequiredService<ModernTransponderRepository>();
	}
}
