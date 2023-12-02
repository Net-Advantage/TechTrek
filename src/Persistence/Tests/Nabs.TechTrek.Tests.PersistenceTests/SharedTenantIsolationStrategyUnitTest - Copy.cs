namespace Nabs.TechTrek.Tests.PersistenceTests;

public class SharedTenantIsolationStrategyUnitTest(ITestOutputHelper testOutputHelper) 
    : ScopedDependencyInversionTestBase(testOutputHelper)
{
    protected override void ConfigureService(ServiceCollection services)
    {
        services.AddDbContextFactory<TechTrekSharedTenantDbContext>(options =>
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            options.UseSqlite(connection);
        });
    }

    [Theory]
    [InlineData("e7fa4888-ee31-4f07-9b79-eca1188d2251", "aa9b2c14-ff4e-46ed-8ebe-e7b96be2cdbf")]
    [InlineData("1951292e-d2f3-4e59-bf24-9ef0294864f2", "ca3608e2-82e0-4f02-a522-d6904839033e")]
    public async Task RunTest(Guid tenantId, Guid userId)
    {
        ApplicationContextFactory = () => new ApplicationContext()
        {
            TenantIsolationStrategy = TenantIsolationStrategy.SharedShared,
            TenantContext = new TenantContext()
            {
                TenantId = tenantId
            }
        };

        ResetDatabase(tenantId, userId);

        var dbContextFactory = ServiceProvider.GetRequiredService<IDbContextFactory<TechTrekSharedTenantDbContext>>();
        var dbContext = dbContextFactory.CreateDbContext();

        // Act
        var tenantComments = await dbContext.WeatherForecastComments
            .AsNoTracking()
            .ToListAsync();

        // Assert
        Assert.Single(tenantComments);
    }

    private void ResetDatabase(Guid tenantId, Guid userId)
    {
        // Arrange
        var dbContextFactory = ServiceProvider.GetRequiredService<IDbContextFactory<TechTrekSharedTenantDbContext>>();
        var dbContext = dbContextFactory.CreateDbContext();
        dbContext.Database.EnsureCreated();

        var date = DateOnly.FromDateTime(DateTime.Now);
        var id = (date.Year * 10000) + (date.Month * 100) + date.Day;

        dbContext.WeatherForecasts.Add(new WeatherForecastEntity
        {
            Id = id,
            Date = date,
            TemperatureC = 20,
            Summary = "Warm"
        });

        var comment = new WeatherForecastCommentEntity
        {
            Id = Guid.NewGuid(),
            WeatherForecastId = id,
            Comment = $"Test comment from {tenantId} - {userId}"
        };
        dbContext.WeatherForecastComments.Add(comment);

        dbContext.SaveChanges();
        dbContext.ChangeTracker.Clear();
    }
}