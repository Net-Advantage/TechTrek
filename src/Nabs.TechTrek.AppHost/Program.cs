using Nabs.TechTrek.ServiceDefaults;

var builder = DistributedApplication
	.CreateBuilder(args);

builder.AddDapr();

//builder
//	.AddAzureProvisioning();

//var keyVault = builder
//	.AddAzureKeyVault(Strings.TechTrekKeyVault);

//var appConfig = builder
//	.AddAzureAppConfiguration(Strings.TechTrekAppConfig);

var api = builder
	.AddProject<Projects.Nabs_TechTrek_WebApi>(Strings.TechTrekWebApi)
	.WithDaprSidecar(Strings.TechTrekWebApi);

var ui = builder
	.AddProject<Projects.Nabs_TechTrek_WebApp>(Strings.TechTrekWebApp)
	.WithDaprSidecar(Strings.TechTrekWebApp);

builder
	.AddProject<Projects.Nabs_TechTrek_Gateway>(Strings.TechTrekGateway);

builder
	.Build()
	.Run();