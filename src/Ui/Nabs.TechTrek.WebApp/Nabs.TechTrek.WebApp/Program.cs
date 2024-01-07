using Nabs.TechTrek.WebApp.Components;
using Nabs.TechTrek.Clients.WeatherClients;
using Nabs.TechTrek.WebApp.Components.Layout;
using Nabs.TechTrek.Identity.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddServiceDefaults();

builder
    .AddWeatherForecastClients();

builder.Services
    .AddHttpContextAccessor();


builder.AddServiceAuthentication(context =>
{
    var isAuthenticated = context.Principal?.Identity?.IsAuthenticated ?? false;

    if (!isAuthenticated)
    {
        return Task.CompletedTask;
    }

    var shellLayoutViewModel = context.HttpContext.RequestServices.GetRequiredService<ShellLayoutViewModel>();
    shellLayoutViewModel.DisplayFullName = context.Principal!.Claims
        .Where(claim => claim.Type == "name")
        .Select(claim => claim.Value)
        .FirstOrDefault()!;

    return Task.CompletedTask;
});

builder.Services
    .AddScoped<ShellLayoutViewModel, ShellLayoutViewModel>();

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
