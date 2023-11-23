namespace Nabs.TechTrek.Tests.PersistenceTests;

public class EfCoreFiltersUnitTest : ScopedDependencyInversionTestBase
{
    private readonly (Guid tenantId, Guid userId)[] _fkIds = 
        [
            (Guid.NewGuid(), Guid.NewGuid()),
            (Guid.NewGuid(), Guid.NewGuid())
        ];

    public EfCoreFiltersUnitTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        
    }

    public override void ConfigureService(ServiceCollection services)
    {
        services.AddDbContextFactory<TechTrekDbContext>(options =>
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            options.UseSqlite(connection);
        });
    }

    [Theory]
    [InlineData(true, 1)]
    [InlineData(false, 2)]
    public void WithFilters(bool withTenantFilter, int countOfResults)
    {
        ApplicationContextFactory = () => new ApplicationContext()
        {
            TenantContext = new TenantContext()
            {
                TenantId = _fkIds[0].tenantId,
                WithTenantFilter = withTenantFilter
            }
        };

        ResetDatabase();

        var dbContextFactory = ServiceProvider.GetRequiredService<IDbContextFactory<TechTrekDbContext>>();
        var dbContext = dbContextFactory.CreateDbContext();

        // Act
        var tenantComments = dbContext.WeatherForecastComments
            .AsNoTracking()
            .ToArray();

        // Assert
        Assert.Equal(countOfResults, tenantComments.Length);
    }

    private void ResetDatabase()
    {
        // Arrange
        var dbContextFactory = ServiceProvider.GetRequiredService<IDbContextFactory<TechTrekDbContext>>();
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
            TenantId = fkId.tenantId,
            UserId = fkId.userId,
            Comment = $"Test comment from {fkId.tenantId} - {fkId.userId}"
        });
        dbContext.WeatherForecastComments.AddRange(comments);

        dbContext.SaveChanges();
        dbContext.ChangeTracker.Clear();
    }
}