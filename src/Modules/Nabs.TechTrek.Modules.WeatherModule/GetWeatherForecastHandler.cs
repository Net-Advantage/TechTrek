using Nabs.TechTrek.Contracts.WeatherContracts;

namespace Nabs.TechTrek.Modules.WeatherModule;

public static class GetWeatherForecastHandler
{
	private static readonly string[] _summaries = [
		"Freezing", 
		"Bracing", 
		"Chilly", 
		"Cool", 
		"Mild", 
		"Warm", 
		"Balmy", 
		"Hot", 
		"Sweltering", 
		"Scorching"
	];

	public static async Task<WeatherForecastResponse> Handle()
	{
		var items = Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = _summaries[Random.Shared.Next(_summaries.Length)]
		})
			.ToArray();

		var result = new WeatherForecastResponse()
		{
			Items = items
		};

		return await Task.FromResult(result);
	}
}

