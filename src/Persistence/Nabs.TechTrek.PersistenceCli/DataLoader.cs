using Microsoft.EntityFrameworkCore;
using Nabs.TechTrek.Persistence;
using Nabs.TechTrek.Persistence.Entities;

namespace Nabs.TechTrek.PersistenceCli;

internal interface IDataLoader
{
    Task EnsureDatabaseCreatedAsync();
    Task LoadGeneralScenarioDataAsync();
    Task LoadTenantScenarioDataAsync(Guid tenantId);
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

    public async Task LoadGeneralScenarioDataAsync()
    {
        var dbContext = _dbContextFactory.CreateDbContext();

        if (!dbContext.WeatherForecasts.Any())
        {
            dbContext.AddItemsFromResourceFile<WeatherForecastEntity>(".WeatherForecastEntityItems.json");
        }
        
        await dbContext.SaveChangesAsync();
    }

    public async Task LoadTenantScenarioDataAsync(Guid tenantId)
    {
        var dbContext = _dbContextFactory.CreateDbContext();

        var tenantIdSegment = tenantId.ToString().Replace('-', '_');
        dbContext.AddItemsFromResourceFile<WeatherForecastCommentEntity>($".Tenants._{tenantIdSegment}.WeatherForecastCommentEntityItems.json");

        await dbContext.SaveChangesAsync();
    }
}
