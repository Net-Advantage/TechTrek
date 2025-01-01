using Microsoft.Extensions.DependencyInjection;

namespace TechTrek.Tenant.Activities;

public static class ActivitiesExtensions
{
    public static void AddActivities(this IServiceCollection services)
    {
        services.AddSingleton<TenantActivityFactory>();
    }
}
