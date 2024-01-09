using Cocona;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nabs.TechTrek.Core.ApplicationContext.Abstractions;
using Nabs.TechTrek.Persistence;
using Nabs.TechTrek.PersistenceCli;

var builder = CoconaApp.CreateBuilder();

// Set up DI

Func<IServiceProvider, IApplicationContext>? ApplicationContextFactory = null;

const string ConnectionString = "Server=localhost,14331;Database={0};User Id=sa;Password=Password123;TrustServerCertificate=True;";

builder.Services.AddScoped<IApplicationContext>(sp =>
{
    ApplicationContextFactory ??= (sp) => new ApplicationContext()
        {
            TenantContext = new TenantContext()
            {
                TenantId = Guid.Empty
            },
            TenantIsolationStrategy = TenantIsolationStrategy.SharedShared
        };

    return ApplicationContextFactory.Invoke(sp);
});

builder.Services.AddDbContextFactory<TechTrekDedicatedTenantDbContext>((sp, options) =>
{
    var applicationContext = sp.GetRequiredService<IApplicationContext>();
    var databaseName = $"TechTrekDb_{applicationContext.TenantIsolationStrategy}_{applicationContext.TenantContext.TenantId}";
    var connectionString = string.Format(ConnectionString, databaseName);
    options.UseSqlServer(connectionString);
});

builder.Services.AddDbContextFactory<TechTrekSharedTenantDbContext>((sp, options) =>
{
    var applicationContext = sp.GetRequiredService<IApplicationContext>();
    var databaseName = $"TechTrekDb_{applicationContext.TenantIsolationStrategy}";
    var connectionString = string.Format(ConnectionString, databaseName);
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

app.AddCommand("SharedSharedReset", async () =>
{
    ApplicationContextFactory = (sp) =>
    {
        return new ApplicationContext()
        {
            TenantContext = new TenantContext()
            {
                TenantId = Guid.Empty
            },
            TenantIsolationStrategy = TenantIsolationStrategy.SharedShared
        };
    };

    IDataLoader dataLoader = app.Services.GetRequiredService<DataLoader<TechTrekSharedTenantDbContext>>();
    await dataLoader.EnsureDatabaseCreatedAsync();
    await dataLoader.LoadGeneralScenarioDataAsync();
});

app.AddCommand("SharedShared", async (Guid tenantId) =>
{
    ApplicationContextFactory = (sp) =>
    {
        return new ApplicationContext()
        {
            TenantContext = new TenantContext()
            {
                TenantId = tenantId
            },
            TenantIsolationStrategy = TenantIsolationStrategy.SharedShared
        };
    };

    IDataLoader dataLoader = app.Services.GetRequiredService<DataLoader<TechTrekSharedTenantDbContext>>();

    await dataLoader.LoadTenantScenarioDataAsync(tenantId);
});

//app.AddCommand(async (TenantIsolationStrategy isolation, Guid tenantId) =>
//{
//    _isolation = isolation;

//    _tenantId = isolation switch
//    {
//        TenantIsolationStrategy.SharedDedicated => tenantId,
//        TenantIsolationStrategy.SharedShared => tenantId,
//        _ => Guid.Empty
//    };

//    IDataLoader dataLoader = isolation switch
//    {
//        TenantIsolationStrategy.DedicatedDedicated => app.Services.GetRequiredService<DataLoader<TechTrekDedicatedTenantDbContext>>(),
//        TenantIsolationStrategy.SharedDedicated => app.Services.GetRequiredService<DataLoader<TechTrekDedicatedTenantDbContext>>(),
//        TenantIsolationStrategy.SharedShared => app.Services.GetRequiredService<DataLoader<TechTrekSharedTenantDbContext>>(),
//        _ => throw new NotSupportedException($"TenantIsolationStrategy {isolation} is not supported.")
//    };

//    Console.WriteLine($"Processing TenantIsolationStrategy: {isolation} ...");

//    await dataLoader.EnsureDatabaseCreatedAsync();
//    await dataLoader.LoadScenarioDataAsync();

//    await Task.CompletedTask;
//});

await app.RunAsync();