var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.Nabs_TechTrek_WebApi>("nabs.techtrek.webapi");

var ui = builder.AddProject<Projects.Nabs_TechTrek_WebApp>("nabs.techtrek.webapp")
	.WithReference(api);

builder.AddProject<Projects.Nabs_TechTrek_Gateway>("nabs.techtrek.gateway")
	.WithReference(api)
	.WithReference(ui);

builder.Build().Run();