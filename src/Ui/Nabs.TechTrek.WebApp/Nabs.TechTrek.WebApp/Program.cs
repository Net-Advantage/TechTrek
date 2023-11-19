using Nabs.TechTrek.WebApp.Components;
using Nabs.TechTrek.Clients.WeatherClients;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddWeatherForecastClients();

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
