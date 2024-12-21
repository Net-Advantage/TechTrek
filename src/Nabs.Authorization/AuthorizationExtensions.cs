using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Nabs.Authorization;

public static class AuthorizationExtensions
{
    public static void AddNabsAuthorization(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var validateIssuer = true;
            var validateAudience = true;
            var issuer = "IssuerName";
            var audience = "AudienceName";
            var validateIssuerSigningKey = true;
            var issuerSigningKey = "superSecretKey@345";
            var issuerSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey));

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = validateIssuer,
                ValidateAudience = validateAudience,
                ValidateIssuerSigningKey = validateIssuerSigningKey,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = issuerSecurityKey
            };
        });
    }

    public static void UseNabsAuthorization(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<ApplicationContextMiddleware>();
    }
}