
using Microsoft.AspNetCore.Http;

namespace Portfolio.CrossCuttingConcerns.Middleware;

public class TestMiddleware
{
    private readonly RequestDelegate _next;

    public TestMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        Console.WriteLine();
        await _next(context);
        Console.WriteLine();
    }
}