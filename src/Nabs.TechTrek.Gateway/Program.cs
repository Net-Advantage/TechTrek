using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Web;
using Nabs.TechTrek.Gateway.Middlewares;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services
	.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
	.AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services
	.AddAuthorization();

builder.Services
	.AddReverseProxy()
	.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
	.AddServiceDiscoveryDestinationResolver();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapDefaultEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<TechTrekCookieAuthMiddleware>();

app.MapReverseProxy();

app.Run();


class TechTrekUser : IdentityUser;