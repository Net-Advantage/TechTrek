using Nabs.TechTrek.Contracts.WeatherContracts;
using System.Net.Http.Json;

namespace Nabs.TechTrek.Clients.WeatherClients;

//TODO: DWS: This is where the Code Generator will generate the client code.

public class WeatherForecastClient(HttpClient httpClient)
{
	private readonly HttpClient _httpClient = httpClient;

	public async Task<WeatherForecastResponse> GetWeatherForecast()
	{
		var result = await _httpClient.GetFromJsonAsync<WeatherForecastResponse>("WeatherForecast");
		return result!;
	}
}
