using Dapr.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Nabs.TechTrek.Clients.WeatherClients;

public static class DependencyInversionExtensions
{
	public static IHostApplicationBuilder AddWeatherForecastClients(this IHostApplicationBuilder builder)
	{
		builder.Services.AddDaprClient();

		builder.Services.AddKeyedSingleton<HttpClient>(Strings.TechTrekWebApi, (sp, name) =>
		{
			return DaprClient.CreateInvokeHttpClient($"{name}");
		});

		builder.Services.AddSingleton<WeatherForecastClient>();

		return builder;
	}
}
