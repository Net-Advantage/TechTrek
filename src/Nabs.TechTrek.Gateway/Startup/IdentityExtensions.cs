using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;

namespace Nabs.TechTrek.Gateway.Startup;

public static class IdentityExtensions
{
	public static WebApplicationBuilder AddIdentity(this WebApplicationBuilder builder)
	{
		builder.Services
			.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
			.AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

		builder.Services
			.AddAuthorization();

		return builder;
	}
}
