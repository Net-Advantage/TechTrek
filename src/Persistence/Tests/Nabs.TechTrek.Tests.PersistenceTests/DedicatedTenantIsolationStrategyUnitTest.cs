namespace Nabs.TechTrek.Tests.PersistenceTests;

public class DedicatedTenantIsolationStrategyUnitTest(
    ITestOutputHelper testOutputHelper, 
    DatabaseFixture fixture) 
    : DatabaseTestBase(testOutputHelper, fixture)
{
    [Fact]
    public async Task RunTest()
    {
        var tenantId = new Guid("731724a1-9b57-46ce-baaf-7325bc8711c0");
        Fixture.ApplicationContextFactory = () => new ApplicationContext()
        {
            TenantIsolationStrategy = TenantIsolationStrategy.DedicatedDedicated,
            TenantContext = new TenantContext()
            {
                TenantId = tenantId
            }
        };

        var dbContextFactory = Fixture.ServiceScope.ServiceProvider.GetRequiredService<IDbContextFactory<TechTrekDbContext>>();
        var dbContext = dbContextFactory.CreateDbContext();

        // Act
        var tenantComments = await dbContext.WeatherForecastComments
            .AsNoTracking()
            .ToListAsync();

        // Assert
        Assert.Single(tenantComments);

        var connectionString = dbContext.Database.GetConnectionString()!;
        WriteLine($"ConnectionString: {connectionString}");
        Assert.Contains($"Database=TechTrekDb_DedicatedDedicated_{tenantId};", connectionString);
    }
}