using Microsoft.EntityFrameworkCore;
using Nabs.TechTrek.Persistence;

namespace Nabs.TechTrek.PersistenceCli;

internal sealed class DataLoader(IDbContextFactory<TechTrekDedicatedTenantDbContext> dbContextFactory)
{
    private readonly IDbContextFactory<TechTrekDedicatedTenantDbContext> _dbContextFactory = dbContextFactory;

    internal async Task EnsureDatabaseCreatedAsync()
    {
        var dbContext = _dbContextFactory.CreateDbContext();
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
    }

    internal async Task LoadScenarioDataAsync()
    {
        var dbContext = _dbContextFactory.CreateDbContext();



        await Task.CompletedTask;
    }
}
