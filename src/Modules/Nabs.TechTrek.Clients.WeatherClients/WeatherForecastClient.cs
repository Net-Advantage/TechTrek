using Microsoft.Extensions.DependencyInjection;
using Nabs.TechTrek.Contracts.WeatherContracts;
using System.Net.Http.Json;

namespace Nabs.TechTrek.Clients.WeatherClients;

//TODO: DWS: This is where the Code Generator will generate the client code.

public class WeatherForecastClient([FromKeyedServices(Strings.TechTrekWebApi)] HttpClient client)
{
    private readonly HttpClient _client = client;

    public async Task<WeatherForecastResponse> GetWeatherForecast()
    {
        var result = await _client
            .GetFromJsonAsync<WeatherForecastResponse>("WeatherForecast");

        return result!;
    }
}
