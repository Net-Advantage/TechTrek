using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nabs.TechTrek.Gateway.Yarp;

public sealed class BearerTokenFactory(BearerTokenSettings bearerTokenSettings)
{
	private readonly BearerTokenSettings _bearerTokenSettings = bearerTokenSettings;

	public string GenerateTokenFromClaims(IEnumerable<Claim> claims)
	{
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_bearerTokenSettings.Secret));
		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
						issuer: _bearerTokenSettings.Issuer,
						audience: _bearerTokenSettings.Audience,
						claims: claims,
						expires: DateTime.UtcNow.AddMinutes(30),
						signingCredentials: credentials);
		try
		{
			var result = new JwtSecurityTokenHandler().WriteToken(token);
			return result;
		}
		catch(Exception ex)
		{
			return $"InvalidToken: {ex.Message}";
		}
	}
}