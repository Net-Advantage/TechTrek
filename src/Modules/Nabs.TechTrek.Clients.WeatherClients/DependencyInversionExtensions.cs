using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Nabs.TechTrek.Clients.WeatherClients;

public static class DependencyInversionExtensions
{
	public static IHostApplicationBuilder AddWeatherForecastClients(this IHostApplicationBuilder builder)
	{
		builder.Services.AddDaprClient();

		builder.Services.AddTransient<WeatherForecastClient>();

		return builder;
	}
}
