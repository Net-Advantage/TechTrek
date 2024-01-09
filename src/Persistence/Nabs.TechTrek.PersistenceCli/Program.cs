using Cocona;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nabs.TechTrek.Core.ApplicationContext.Abstractions;
using Nabs.TechTrek.Persistence;
using Nabs.TechTrek.PersistenceCli;
using System.Runtime.InteropServices;

var builder = CoconaApp.CreateBuilder();

// Set up DI
var _tenantId = Guid.Empty;
const string ConnectionString = "Server=localhost,14331;Database=TechTrekDb_{0};User Id=sa;Password=Password123;TrustServerCertificate=True;";

builder.Services.AddSingleton<IApplicationContext>((sp) => new ApplicationContext()
{
    TenantContext = new TenantContext()
    {
        TenantId = _tenantId
    }
});

builder.Services.AddDbContextFactory<TechTrekDedicatedTenantDbContext>(options =>
{
    var connectionString = string.Format(ConnectionString, "TechTrekDb_Dedicated");
    options.UseSqlServer(connectionString);
});

builder.Services.AddDbContextFactory<TechTrekSharedTenantDbContext>(options =>
{
    var connectionString = string.Format(ConnectionString, "TechTrekDb_Shared");
    options.UseSqlServer(connectionString);
});

builder.Services.AddSingleton<DataLoader<TechTrekDedicatedTenantDbContext>>(sp =>
{
    var dbContextFactory = sp.GetRequiredService<IDbContextFactory<TechTrekDedicatedTenantDbContext>>(); 
    return new DataLoader<TechTrekDedicatedTenantDbContext>(dbContextFactory!);
});

builder.Services.AddSingleton<DataLoader<TechTrekSharedTenantDbContext>>(sp =>
{
    var dbContextFactory = sp.GetRequiredService<IDbContextFactory<TechTrekSharedTenantDbContext>>(); 
    return new DataLoader<TechTrekSharedTenantDbContext>(dbContextFactory!);
});

var app = builder.Build();

app.AddCommand(async (TenantIsolationStrategy isolation, Guid tenantId) =>
{
    _tenantId = isolation switch
    {
        TenantIsolationStrategy.SharedDedicated => tenantId,
        TenantIsolationStrategy.SharedShared => tenantId,
        _ => Guid.Empty
    };

    IDataLoader dataLoader = isolation switch
    {
        TenantIsolationStrategy.DedicatedDedicated => app.Services.GetRequiredService<DataLoader<TechTrekDedicatedTenantDbContext>>(),
        TenantIsolationStrategy.SharedDedicated => app.Services.GetRequiredService<DataLoader<TechTrekDedicatedTenantDbContext>>(),
        TenantIsolationStrategy.SharedShared => app.Services.GetRequiredService<DataLoader<TechTrekSharedTenantDbContext>>(),
        _ => throw new NotSupportedException($"TenantIsolationStrategy {isolation} is not supported.")
    };

    Console.WriteLine($"Processing TenantIsolationStrategy: {isolation} ...");

    await dataLoader.EnsureDatabaseCreatedAsync();
    await dataLoader.LoadScenarioDataAsync();

    await Task.CompletedTask;
});

await app.RunAsync();