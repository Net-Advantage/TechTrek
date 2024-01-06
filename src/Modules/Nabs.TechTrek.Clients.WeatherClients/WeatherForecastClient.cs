using Dapr.Client;
using Nabs.TechTrek.Contracts.WeatherContracts;

namespace Nabs.TechTrek.Clients.WeatherClients;

//TODO: DWS: This is where the Code Generator will generate the client code.

public class WeatherForecastClient(DaprClient client)
{
	private readonly DaprClient _client = client;

	public async Task<WeatherForecastResponse> GetWeatherForecast()
	{
		var result = await _client
			.InvokeMethodAsync<WeatherForecastResponse>(HttpMethod.Get, "techTrekWebApi", "WeatherForecast");

		return result!;
	}
}
