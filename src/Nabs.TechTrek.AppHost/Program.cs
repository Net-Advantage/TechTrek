
var builder = DistributedApplication
	.CreateBuilder(args);

var stateStore = builder.AddDaprStateStore(Strings.TechTrekStateStore);
var pubSub = builder.AddDaprPubSub(Strings.TechTrekPubSub);

var api = builder
	.AddProject<Projects.Nabs_TechTrek_WebApi>(Strings.TechTrekWebApi)
	.WithDaprSidecar(new DaprSidecarOptions 
	{ 
		AppId = Strings.TechTrekWebApi,
		ResourcesPaths = [ "../Nabs.TechTrek.AppHost/DaprComponents/Local" ],
		AppProtocol = "http",
		AppPort = 5289,
		Config = "../Nabs.TechTrek.AppHost/DaprComponents/config.yaml"
	})
	.WithReference(stateStore)
	.WithReference(pubSub);

var ui = builder
	.AddProject<Projects.Nabs_TechTrek_WebApp>(Strings.TechTrekWebApp)
	.WithDaprSidecar(new DaprSidecarOptions 
	{ 
		AppId = Strings.TechTrekWebApp 
	})
	.WithReference(stateStore);

builder
	.AddProject<Projects.Nabs_TechTrek_Gateway>(Strings.TechTrekGateway)
	.WithReference(api)
	.WithReference(ui);

using var app = builder.Build();

await app.RunAsync();