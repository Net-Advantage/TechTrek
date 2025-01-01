using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Nabs.Tests;

public abstract class BaseTestFixture : IAsyncLifetime
{
    public IServiceProvider ServiceProvider { get; private set; } = default!;

    abstract protected void ConfigureServices(IServiceCollection services, IConfigurationRoot configurationRoot);
    protected IConfigurationRoot ConfigurationRoot { get; private set; } = default!;


    public Task InitializeAsync()
    {
        // Set up the configuration
        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        ConfigurationRoot = configurationBuilder.Build();

        // Set up the service collection
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection, ConfigurationRoot);

        // Build the service provider
        ServiceProvider = serviceCollection.BuildServiceProvider();

        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        if (ServiceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
        return Task.CompletedTask;
    }
}
