namespace Nabs.TechTrek.Tests.PersistenceTests;

public class DedicatedTenantIsolationStrategyUnitTest(ITestOutputHelper testOutputHelper) 
    : ScopedDependencyInversionTestBase(testOutputHelper)
{
    private readonly (Guid tenantId, Guid userId)[] _fkIds = 
        [
            (Guid.NewGuid(), Guid.NewGuid()),
            (Guid.NewGuid(), Guid.NewGuid())
        ];

    protected override void ConfigureService(ServiceCollection services)
    {
        services.AddDbContextFactory<TechTrekDedicatedTenantDbContext>(options =>
        {
            var connectionString = "Server=localhost,14331;Database=TechTrekDb_Dedicated;User Id=sa;Password=Password123;TrustServerCertificate=True;";
            options.UseSqlServer(connectionString);
        });
    }

    [Fact]
    public void RunTest()
    {
        ApplicationContextFactory = () => new ApplicationContext()
        {
            TenantIsolationStrategy = TenantIsolationStrategy.SharedDedicated,
            TenantContext = new TenantContext()
            {
                TenantId = _fkIds[0].tenantId
            }
        };

        ResetDatabase();

        var dbContextFactory = ServiceProvider.GetRequiredService<IDbContextFactory<TechTrekDedicatedTenantDbContext>>();
        var dbContext = dbContextFactory.CreateDbContext();

        // Act
        var tenantComments = dbContext.WeatherForecastComments
            .AsNoTracking()
            .ToArray();

        // Assert
        Assert.Equal(2, tenantComments.Length);
    }

    private void ResetDatabase()
    {
        // Arrange
        var dbContextFactory = ServiceProvider.GetRequiredService<IDbContextFactory<TechTrekDedicatedTenantDbContext>>();
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

        var comments = _fkIds.Select(fkId => new WeatherForecastCommentEntity
        {
            Id = Guid.NewGuid(),
            WeatherForecastId = id,
            UserId = fkId.userId,
            Comment = $"Test comment from {fkId.tenantId} - {fkId.userId}"
        });
        dbContext.WeatherForecastComments.AddRange(comments);

        dbContext.SaveChanges();
        dbContext.ChangeTracker.Clear();
    }
}