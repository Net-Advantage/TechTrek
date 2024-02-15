namespace Nabs.TechTrek.WebApp;

public class TechTrekAuthenticationStateProvider(
	IHttpContextAccessor httpContextAccessor,
	ShellLayoutViewModel shellLayoutViewModel) 
	: AuthenticationStateProvider
{
	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
	private readonly ShellLayoutViewModel _shellLayoutViewModel = shellLayoutViewModel;

	public override Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var user = _httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal(new ClaimsIdentity());
		var isAuthenticated = user.Identity?.IsAuthenticated ?? false;

		if (isAuthenticated)
		{
			_shellLayoutViewModel.DisplayFullName = user!.Claims
				.Where(claim => claim.Type == "name")
				.Select(claim => claim.Value)
				.FirstOrDefault()!;
		}

		return Task.FromResult(new AuthenticationState(user));
	}
}
