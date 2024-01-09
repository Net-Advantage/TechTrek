namespace Nabs.TechTrek.Tests.PersistenceTests;

public sealed class SharedDatabaseFixture : IDisposable
{
    private readonly IServiceCollection _services = new ServiceCollection();

    public SharedDatabaseFixture(IMessageSink messageSink)
    {
        _services.AddDbContextFactory<TechTrekDbContext>(options =>
        {
            var connectionString = "Server=localhost,14331;Database=TechTrekDb;User Id=sa;Password=Password123;TrustServerCertificate=True;";
            options.UseSqlServer(connectionString);

            options.LogTo(s =>
            {
                var message = new DiagnosticMessage(s);
                messageSink.OnMessage(message);
            });
        });

        _services.TryAddScoped<IApplicationContext>((sp) =>
            ApplicationContextFactory?.Invoke() ?? new ApplicationContext()
            {
                TenantContext = new TenantContext()
                {
                    TenantId = Guid.NewGuid()
                }
            });

        ServiceProvider = _services
            .BuildServiceProvider()
            .CreateScope()
            .ServiceProvider;
    }

    public IServiceProvider ServiceProvider { get; private set; }

    public Func<IApplicationContext>? ApplicationContextFactory { get; set; }

    public void ResetDatabase((Guid tenantId, Guid userId)[] fkIds)
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

        var comments = fkIds.Select(fkId => new WeatherForecastCommentEntity
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

    public void Dispose()
    {

    }
}