using Microsoft.AspNetCore.Http;
using Nabs.Application;

namespace Nabs.Authorization;

public class ApplicationContextMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IRequestContext requestContext)
    {
        requestContext.SetPrincipal(context.User);
        await _next(context);
    }
}