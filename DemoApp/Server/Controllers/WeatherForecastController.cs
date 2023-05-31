namespace DemoApp.Server.Controllers;

using DemoApp.Shared;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
	private static readonly string[] Summaries =
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	private readonly ILogger<WeatherForecastController> _logger;

	public WeatherForecastController(ILogger<WeatherForecastController> logger)
	{
		this._logger = logger;
	}

	[HttpGet]
	public IEnumerable<WeatherForecast> Get()
	{
		return Enumerable.Range(1, 5)
		.Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = WeatherForecastController.Summaries[Random.Shared.Next(WeatherForecastController.Summaries.Length)]
			})
		.ToArray();
	}
}
