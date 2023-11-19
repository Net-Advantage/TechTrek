using Microsoft.Extensions.DependencyInjection;

namespace Nabs.TechTrek.Modules.WeatherModule;

public static class DependencyInversionExtensions
{
    public static IServiceCollection AddWeatherModule(this IServiceCollection services)
    {
        services.AddScoped<WeatherService>();

        return services;
    }
}
