using Nabs.TechTrek.ServiceDefaults;

var builder = DistributedApplication
	.CreateBuilder(args);

builder
	.AddAzureProvisioning();

var keyVault = builder
	.AddAzureKeyVault(Strings.TechTrekKeyVault);

var appConfig = builder
	.AddAzureAppConfiguration(Strings.TechTrekAppConfig);

var sqlDb = builder
	.AddSqlServerConnection(Strings.TechTrekSqlServer);

var api = builder
	.AddProject<Projects.Nabs_TechTrek_WebApi>(Strings.TechTrekWebApi)
	.WithReference(sqlDb)
	.WithReference(appConfig);

var ui = builder
	.AddProject<Projects.Nabs_TechTrek_WebApp>(Strings.TechTrekWebApp)
	.WithReference(api);

builder
	.AddProject<Projects.Nabs_TechTrek_Gateway>(Strings.TechTrekGateway)
	.WithReference(api)
	.WithReference(ui)
	.WithReference(appConfig);

builder
	.Build()
	.Run();