using Microsoft.AspNetCore.Builder;
using Portfolio.CrossCuttingConcerns.Middleware;

namespace Portfolio.CrossCuttingConcerns;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCrossCuttingMiddleware(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<RequestLoggingMiddleware>();

        applicationBuilder.UseMiddleware<ExceptionHandlerMiddleware>();

        return applicationBuilder;
        
    }
}