namespace VehicleTransponderSystem.Infrastructure.Logging
{
	public class TraceLogger : ILogger
	{
		public void Log(string message)
		{
			Console.WriteLine($"[TRACE] {DateTime.Now}: {message}");
		}
	}
}
