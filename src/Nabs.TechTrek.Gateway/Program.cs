using Nabs.TechTrek.Gateway.Startup;

var builder = WebApplication.CreateBuilder(args);

builder
	.AddServiceDefaults();

builder.Services
	.AddDaprClient();

builder.Services
	.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder
	.AddIdentity()
	.AddYarp();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.Run();
