﻿

namespace Nabs.TechTrek.Gateway.Startup;

public static class IdentityExtensions
{
	public static WebApplicationBuilder AddIdentity(this WebApplicationBuilder builder)
	{
		var azureAdSection = builder.Configuration.GetRequiredSection("AzureAd")!;
		
		builder.Services
			.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
			.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
			{
				SetupJwtBearerOptions(builder, options);
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

	private static void SetupJwtBearerOptions(WebApplicationBuilder builder, JwtBearerOptions options)
	{
		var jwtBearerAuthenticationSection = builder.Configuration.GetRequiredSection("JwtBearerAuthentication")!;
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
	}
}
