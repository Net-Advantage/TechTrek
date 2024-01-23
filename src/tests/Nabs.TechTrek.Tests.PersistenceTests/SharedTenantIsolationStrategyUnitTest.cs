namespace Nabs.TechTrek.Tests.PersistenceTests;

public class SharedTenantIsolationStrategyUnitTest(
    ITestOutputHelper testOutputHelper, 
    DatabaseFixture fixture) 
    : DatabaseTestBase(testOutputHelper, fixture)
{

    protected override void BeforeTestRun()
    {
        ConfigureServices = (services, configuration) =>
        {
            services.AddPersistence(configuration);
        };

        base.BeforeTestRun();
    }

    [Theory]
    [InlineData("731724a1-9b57-46ce-baaf-7325bc8711c0", "aa9b2c14-ff4e-46ed-8ebe-e7b96be2cdbf")]
    [InlineData("931d3b9a-4931-4577-bbe0-dc913db3d3c9", "ca3608e2-82e0-4f02-a522-d6904839033e")]
    public async Task RunTest(Guid tenantId, Guid userId)
    {
        _ = userId;

        Fixture.ApplicationContextFactory = () => new ApplicationContext()
        {
            TenantIsolationStrategy = TenantIsolationStrategy.SharedShared,
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
        Assert.Contains($"Database=TechTrekDb_SharedShared", connectionString);
    }
}