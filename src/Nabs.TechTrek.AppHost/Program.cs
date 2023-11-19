var builder = DistributedApplication
	.CreateBuilder(args);

var api = builder
	.AddProject<Projects.Nabs_TechTrek_WebApi>("techtrekwebapi");

var ui = builder
	.AddProject<Projects.Nabs_TechTrek_WebApp>("techtrekwebapp")
	.WithReference(api);

builder
	.AddProject<Projects.Nabs_TechTrek_Gateway>("techtrekgateway")
	.WithReference(api)
	.WithReference(ui);

builder
	.Build()
	.Run();