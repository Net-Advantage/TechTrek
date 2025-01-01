var builder = CoconaApp.CreateBuilder();

// Set up DI
Func<IServiceProvider, IApplicationContext>? ApplicationContextFactory = null;

builder.Services.AddScoped<IApplicationContext>(sp =>
{
	ApplicationContextFactory ??= Helpers
		.CreateApplicationContextFactory(TenantIsolationStrategy.SharedShared, Guid.Empty);

	return ApplicationContextFactory.Invoke(sp);
});


builder.Services.AddSingleton<DataLoader<TechTrekDbContext>>(sp =>
{
	var dbContextFactory = sp.GetRequiredService<IDbContextFactory<TechTrekDbContext>>();
	return new DataLoader<TechTrekDbContext>(dbContextFactory!);
});

var app = builder.Build();

app.AddCommand("Reset", async ([Option('i')] TenantIsolationStrategy isolation, [Option('t')] Guid tenantId) =>
{
	if (isolation == TenantIsolationStrategy.SharedShared)
	{
		Console.WriteLine("SharedShared requires the tenantId to be Empty when the database is reset.");
		tenantId = Guid.Empty;
	}

	ApplicationContextFactory = Helpers
		.CreateApplicationContextFactory(isolation, tenantId);

	var dataLoader = app.Services.GetRequiredService<DataLoader<TechTrekDbContext>>();

	await dataLoader.EnsureDatabaseCreatedAsync();
	await dataLoader.LoadGeneralScenarioDataAsync();
})
.WithDescription("Reset command");


app.AddCommand("Load", async ([Option('i')] TenantIsolationStrategy isolation, [Option('t')] Guid tenantId) =>
{
	ApplicationContextFactory = Helpers
		.CreateApplicationContextFactory(isolation, tenantId);

	var dataLoader = app.Services.GetRequiredService<DataLoader<TechTrekDbContext>>();

	var tenantEntity = await dataLoader.EnsureValidTenantExistsAsync(isolation, tenantId);

	await dataLoader.LoadTenantScenarioDataAsync(tenantId);
})
.WithDescription("Load command");

await app.RunAsync();
