using Google.Api;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Nabs.TechTrek.Gateway.Startup;

public static class IdentityExtensions
{
	public static WebApplicationBuilder AddIdentity(this WebApplicationBuilder builder)
	{
		var azureAdSection = builder.Configuration.GetSection("AzureAd");
		var jwtBearerAuthenticationSection = builder.Configuration.GetSection("JwtBearerAuthentication");
		

		builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
			.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
			{
				var jwtBearerAuthenticationOptions = new JwtBearerAuthenticationOptions();
				jwtBearerAuthenticationSection.Bind(jwtBearerAuthenticationOptions);

				options.MetadataAddress = jwtBearerAuthenticationOptions.MetadataAddress;
				options.RequireHttpsMetadata = jwtBearerAuthenticationOptions.RequireHttpsMetadata;
				options.SaveToken = jwtBearerAuthenticationOptions.SaveToken;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = jwtBearerAuthenticationOptions.ValidIssuer,
					ValidateAudience = true,
					ValidAudience = jwtBearerAuthenticationOptions.ValidAudience
				};

				options.Events = new()
				{
					OnAuthenticationFailed = context =>
					{
						context.Response.StatusCode = 401;
						return Task.CompletedTask;
					},
					OnMessageReceived = context =>
					{
						return Task.CompletedTask;
					},
					OnForbidden = context =>
					{
						return Task.CompletedTask;
					},
					OnChallenge = context =>
					{
						return Task.CompletedTask;
					},
					OnTokenValidated = context =>
					{
						return Task.CompletedTask;
					}
				};
			})
			.AddMicrosoftIdentityWebApp(azureAdSection);

		_ = builder.Services
			.AddAuthorization(options =>
			{
				options.AddPolicy("JwtBearerPolicy", policy =>
				{
					policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
					policy.RequireAuthenticatedUser();
				});
			});

		return builder;
	}
}
