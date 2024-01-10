using Microsoft.Extensions.Configuration;

namespace Nabs.TechTrek.Tests.PersistenceTests;

public sealed class DatabaseFixture : IDisposable
{
    private readonly IServiceCollection _services = new ServiceCollection();
    
    public DatabaseFixture(IMessageSink messageSink)
    {
        //TODO: DWS: Come back to this.
        _ = messageSink;
    }

    public IServiceScope ServiceScope { get; private set; } = default!;
    public IConfigurationRoot Configuration { get; private set; } = default!;

    public Func<IApplicationContext>? ApplicationContextFactory { get; set; }

    public void CreateScope(ITestOutputHelper testOutputHelper)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();

        Configuration = builder.Build();

        _services.AddLogging(_ =>
        {
            _.ClearProviders();
            if (testOutputHelper is not null)
            {
                var provider = new XUnitTestOutputLoggerProvider(testOutputHelper);
                _.AddProvider(provider);
            }
        });

        _services.AddPersistence(Configuration);

        _services.TryAddTransient<IApplicationContext>((sp) =>
            ApplicationContextFactory?.Invoke() ?? new ApplicationContext()
            {
                TenantContext = new TenantContext()
                {
                    TenantId = Guid.NewGuid()
                }
            });

        ServiceScope = _services
            .BuildServiceProvider()
            .CreateScope();
    }

    public void Dispose()
    {
        ServiceScope = null!;
    }
}