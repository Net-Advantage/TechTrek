using Nabs.TechTrek.WebApp.Components;
using Nabs.TechTrek.Clients.WeatherClients;
using Nabs.Ui.Shell;
using Nabs.TechTrek.WebApp.Components.Pages;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddWeatherForecastClients();

builder.Services.AddScoped<IGlobalPageContext, GlobalPageContext>();

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
