
namespace RetailSample.Workflows.UserManagementScenarios;

public static class DependencyInversionExtensions
{
    public static IServiceCollection AddUserUserManagementWorkflows(this IServiceCollection services)
    {
        services.AddSingleton<NewUserWorkflowRepository>();

        return services;
    }
}