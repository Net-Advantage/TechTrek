namespace Nabs.TechTrek.Gateway.Startup;

public sealed class JwtBearerAuthenticationOptions
{
	public string MetadataAddress { get; set; } = default!;
	public bool RequireHttpsMetadata { get; set; }
	public bool SaveToken { get; set; }
	public bool ValidateIssuer { get; set; }
	public string ValidIssuer { get; set; } = default!;
	public bool ValidateAudience { get; set; }
	public string ValidAudience { get; set; } = default!;
}
