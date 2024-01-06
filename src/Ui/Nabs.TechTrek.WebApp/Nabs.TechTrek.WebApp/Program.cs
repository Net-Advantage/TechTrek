using Nabs.TechTrek.WebApp.Components;
using Nabs.TechTrek.Clients.WeatherClients;
using Nabs.TechTrek.WebApp.Components.Layout;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Nabs.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder
	.AddServiceDefaults();

builder
	.AddWeatherForecastClients();

builder.Services
	.AddHttpContextAccessor();


//TODO: DWS: Abstract this.
builder.Services
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		var bearerTokenSettingsSection = builder.Configuration.GetRequiredSection("BearerTokenSettings");
		var bearerTokenSettings = new BearerTokenSettings();
		bearerTokenSettingsSection.Bind(bearerTokenSettings);

		options.TokenValidationParameters = new()
		{
			ValidateIssuer = true,
			ValidIssuer = bearerTokenSettings.Issuer,
			ValidateAudience = true,
			ValidAudience = bearerTokenSettings.Audience,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(bearerTokenSettings.Secret)),
			ValidateLifetime = true,
			ClockSkew = TimeSpan.FromMinutes(1)
		};

		options.Events = new()
		{
			OnTokenValidated = context =>
			{
				var isAuthenticated = context.Principal?.Identity?.IsAuthenticated ?? false;

				if(!isAuthenticated)
				{
					//TODO: DWS: Decide what to do here.

					return Task.CompletedTask;
				}

				var shellLayoutViewModel = context.HttpContext.RequestServices.GetRequiredService<ShellLayoutViewModel>();
				shellLayoutViewModel.DisplayFullName = context.Principal!.Claims
					.Where(claim => claim.Type == "name")
					.Select(claim => claim.Value)
					.FirstOrDefault()!;

				return Task.CompletedTask;
			}
		};
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
