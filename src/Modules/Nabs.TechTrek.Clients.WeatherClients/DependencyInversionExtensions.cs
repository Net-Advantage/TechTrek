using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Nabs.TechTrek.Clients.WeatherClients;

public static class DependencyInversionExtensions
{
	public static IHostApplicationBuilder AddWeatherForecastClients(this IHostApplicationBuilder builder)
	{
		builder.Services.AddHttpClient<WeatherForecastClient>(c =>
		{
			c.BaseAddress = new Uri("http://nabs.techtrek.webapi");
		});

		return builder;
	}
}
