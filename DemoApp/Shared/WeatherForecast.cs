namespace DemoApp.Shared;

public class WeatherForecast
{
	public DateOnly Date { get; set; }

	public int TemperatureC { get; set; }

	public string? Summary { get; set; }

	public int TemperatureF => 32 + (int)(this.TemperatureC / 0.5556);
}
