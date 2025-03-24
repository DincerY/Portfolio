using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Portfolio.CrossCuttingConcerns.Middleware;

public class CustomAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public CustomAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.User.Identity?.IsAuthenticated ?? false)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: Token is missing or invalid.");
            return;
        }

        var roleClaim = context.User.Claims.FirstOrDefault(cl => cl.Type == "role").Value;

        if (roleClaim != "admin")
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Forbidden: You do not have the required role.");
            return;
        }

        await _next(context);
    }
}