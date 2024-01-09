using Microsoft.EntityFrameworkCore;
using Nabs.TechTrek.Persistence;
using Nabs.TechTrek.Persistence.Entities;

namespace Nabs.TechTrek.PersistenceCli;

internal interface IDataLoader
{
    Task EnsureDatabaseCreatedAsync();
    Task LoadScenarioDataAsync();
}

internal sealed class DataLoader<TDbContext>(IDbContextFactory<TDbContext> dbContextFactory)
    : IDataLoader
    where TDbContext : TechTrekDbContext
{
    private readonly IDbContextFactory<TDbContext> _dbContextFactory = dbContextFactory;

    public async Task EnsureDatabaseCreatedAsync()
    {
        var dbContext = _dbContextFactory.CreateDbContext();
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
    }

    public async Task LoadScenarioDataAsync()
    {
        var dbContext = _dbContextFactory.CreateDbContext();

        var weatherForecastEntityItems = "WeatherForecastEntityItems.json"
            .LoadResource<WeatherForecastEntity[]>();
        dbContext.AddRange(weatherForecastEntityItems);

        var weatherForecastCommentEntityItems = "WeatherForecastCommentEntityItems.json"
            .LoadResource<WeatherForecastCommentEntity[]>();
        dbContext.AddRange(weatherForecastCommentEntityItems);

        await dbContext.SaveChangesAsync();
    }
}
