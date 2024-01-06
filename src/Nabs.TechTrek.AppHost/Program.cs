using Nabs.TechTrek.ServiceDefaults;

var builder = DistributedApplication
	.CreateBuilder(args);

var stateStore = builder.AddDaprStateStore(Strings.TechTrekStateStore);
var pubSub = builder.AddDaprPubSub(Strings.TechTrekPubSub);

var api = builder
	.AddProject<Projects.Nabs_TechTrek_WebApi>(Strings.TechTrekWebApi)
	.WithDaprSidecar()
	.WithReference(stateStore)
	.WithReference(pubSub);

var ui = builder
	.AddProject<Projects.Nabs_TechTrek_WebApp>(Strings.TechTrekWebApp)
	.WithDaprSidecar()
	.WithReference(stateStore);

builder
	.AddProject<Projects.Nabs_TechTrek_Gateway>(Strings.TechTrekGateway)
	.WithReference(api)
	.WithReference(ui)
	.WithDaprSidecar()
	.WithReference(stateStore);

using var app = builder.Build();

await app.RunAsync();