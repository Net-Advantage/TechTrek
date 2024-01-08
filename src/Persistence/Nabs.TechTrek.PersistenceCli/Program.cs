using Cocona;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nabs.TechTrek.Core.ApplicationContext.Abstractions;
using Nabs.TechTrek.Persistence;
using Nabs.TechTrek.PersistenceCli;

var builder = CoconaApp.CreateBuilder();

// Set up DI

builder.Services.AddSingleton<IApplicationContext>(new ApplicationContext()
{
    TenantContext = new TenantContext()
    {
        TenantId = Guid.NewGuid()
    }
});

builder.Services.AddDbContextFactory<TechTrekDedicatedTenantDbContext>(options =>
{
    var connectionString = "Server=localhost,14331;Database=TechTrekDb;User Id=sa;Password=Password123;TrustServerCertificate=True;";
    options.UseSqlServer(connectionString);

    //options.LogTo(s =>
    //{

    //});
});

builder.Services.AddSingleton<DataLoader>();

CoconaApp? app = null;

try
{
    app = builder.Build();
}
catch (Exception exx)
{
    _ = exx;
    return;
}

app.AddCommand(async (Environment env, DataLoader dataLoader) =>
{
    Console.WriteLine($"Processing environment: {env} ...");

    await dataLoader.EnsureDatabaseCreatedAsync();



    await Task.CompletedTask;
});

await app.RunAsync();

public enum Environment
{
    Localhost,
    Workflow
}

