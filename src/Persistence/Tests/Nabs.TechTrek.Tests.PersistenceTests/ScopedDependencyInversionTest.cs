using System.Diagnostics;

namespace Nabs.TechTrek.Tests.PersistenceTests;

public abstract class ScopedDependencyInversionTestBase : IAsyncLifetime
{
    private IServiceScope _serviceScope = default!;
    private readonly ITestOutputHelper _testOutputHelper;

    protected ScopedDependencyInversionTestBase(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    protected Func<IApplicationContext>? ApplicationContextFactory { get; set; }
    protected IServiceProvider ServiceProvider { get; set; } = default!;

    public Task InitializeAsync()
    {
        var services = new ServiceCollection();

        services.AddLogging(_ =>
        {
            _.ClearProviders();
            if (_testOutputHelper is not null)
            {
                var provider = new XUnitTestOutputLoggerProvider(_testOutputHelper);
                _.AddProvider(provider);
            }
        });
        
        services.TryAddScoped<IApplicationContext>((sp) => {
            var result = ApplicationContextFactory?.Invoke() ?? 
                throw new UnreachableException();
            return result;
        });

        ConfigureService(services);

        _serviceScope = services
            .BuildServiceProvider()
            .CreateScope();

        ServiceProvider = _serviceScope.ServiceProvider;

        BeforeTestRun();

        return Task.CompletedTask;
    }

    public virtual void ConfigureService(ServiceCollection services)
    {
        
    }

    public virtual void BeforeTestRun()
    {
        
    }

    public Task DisposeAsync()
    {
        ServiceProvider = null;
        _serviceScope.Dispose();

        return Task.CompletedTask;
    }
}
