namespace Nabs.TechTrek.Tests.PersistenceTests;

public class SharedTenantIsolationStrategyUnitTest(
    ITestOutputHelper testOutputHelper, 
    DatabaseFixture fixture) 
    : DatabaseTestBase(testOutputHelper, fixture)
{

    [Theory]
    [InlineData("731724a1-9b57-46ce-baaf-7325bc8711c0", "aa9b2c14-ff4e-46ed-8ebe-e7b96be2cdbf")]
    [InlineData("931d3b9a-4931-4577-bbe0-dc913db3d3c9", "ca3608e2-82e0-4f02-a522-d6904839033e")]
    [InlineData("731724a1-9b57-46ce-baaf-7325bc8711c0", "c6f69b14-5e1e-4ca1-a14c-ec8b2c77affe")]
    [InlineData("931d3b9a-4931-4577-bbe0-dc913db3d3c9", "3574afff-e477-45b9-81de-d17584d33ff1")]
    [InlineData("731724a1-9b57-46ce-baaf-7325bc8711c0", "e04b119e-cc8b-40a7-ba90-cbeacfc3fa88")]
    [InlineData("931d3b9a-4931-4577-bbe0-dc913db3d3c9", "9c0416ac-bda2-47dd-966e-9fffd5f33f1d")]
    [InlineData("731724a1-9b57-46ce-baaf-7325bc8711c0", "a2153945-29a5-4a1a-acb9-9a200eeaf744")]
    [InlineData("931d3b9a-4931-4577-bbe0-dc913db3d3c9", "c4f769a2-4821-44f5-9757-40064079abfc")]
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