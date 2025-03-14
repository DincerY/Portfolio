using Microsoft.AspNetCore.Http;

namespace Portfolio.CrossCuttingConcerns.Middleware;

public class UnauthorizedMiddleware
{
    private readonly RequestDelegate _next;

    public UnauthorizedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            throw new UnauthorizedAccessException("Authorization is failed.");
        }
    }
}