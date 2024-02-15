using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Nabs.Core.Application.Abstractions;

public static class DependencyInversionExtensions
{
	public static IHostApplicationBuilder AddServiceAuthentication(
		this IHostApplicationBuilder builder,
		Func<TokenValidatedContext, Task> onTokenValidated)
	{
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
					OnTokenValidated = onTokenValidated,
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
					}
				};
			});

		return builder;
	}
}
