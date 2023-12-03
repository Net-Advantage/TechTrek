namespace Nabs.TechTrek.Gateway.Middlewares;

public class TechTrekCookieAuthMiddleware
{
	private readonly RequestDelegate _next;

    public TechTrekCookieAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if(!context.User.Identity?.IsAuthenticated ?? true)
        {
            return;
        }

        // Check if JWT token is available and valid, generate if necessary
        var jwtToken = GenerateJwtToken(context);

        // Add JWT token to the request headers
        context.Request.Headers.Add("Authorization", "Bearer " + jwtToken);

        await _next(context);
    }

    private string GenerateJwtToken(HttpContext context)
    {
        // Implement JWT token generation logic here
        // Extract user claims from the cookie and create a JWT token
        // You can use libraries like Microsoft.Identity.Web or System.IdentityModel.Tokens.Jwt for token generation
        // Return the generated token
        return "TheJwtToken";
    }
}
