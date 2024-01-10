using Microsoft.EntityFrameworkCore;
using Nabs.TechTrek.Core.ApplicationContext.Abstractions;
using Nabs.TechTrek.Persistence;
using Nabs.TechTrek.Persistence.Entities;
using System.Globalization;

namespace Nabs.TechTrek.PersistenceCli;

internal interface IDataLoader
{
    Task EnsureDatabaseCreatedAsync();
    Task<TenantEntity> EnsureValidTenantExistsAsync(TenantIsolationStrategy isolationStrategy, Guid tenantId);
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

    public async Task<TenantEntity> EnsureValidTenantExistsAsync(TenantIsolationStrategy isolationStrategy, Guid tenantId)
    {
        var dbContext = _dbContextFactory.CreateDbContext();

        var tenantEntity = await dbContext.Tenants
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == tenantId);

        if (tenantEntity is not null)
        {
            throw new Exception("This tenant has already been loaded.");
        }

        tenantEntity = new TenantEntity()
        {
            Id = tenantId,
            Name = $"Tenant_{tenantId}",
            IsolationStrategy = isolationStrategy
        };

        dbContext.Tenants.Add(tenantEntity);

        await dbContext.SaveChangesAsync();

        return tenantEntity;
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
