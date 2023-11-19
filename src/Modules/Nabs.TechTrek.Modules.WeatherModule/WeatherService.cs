using Nabs.TechTrek.Contracts.WeatherContracts;

namespace Nabs.TechTrek.Modules.WeatherModule;

public class WeatherService
{
	private static readonly string[] Summaries = [
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	];

	public async Task<WeatherForecastResponse> GetWeatherForecast()
	{
		var items = Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		})
			.ToArray();

		var result = new WeatherForecastResponse()
		{
			Items = items
		};

		return await Task.FromResult(result);
	}
}

