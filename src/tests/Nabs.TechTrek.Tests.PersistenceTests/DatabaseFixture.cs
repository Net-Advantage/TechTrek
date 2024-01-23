namespace Nabs.TechTrek.Tests.PersistenceTests;

public class DatabaseFixture : DatabaseFixtureBase
{
    public DatabaseFixture(IMessageSink messageSink) : base(messageSink)
    {
    }

    public override void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddPersistence(configuration);
    }
}