using Microsoft.AspNetCore.Builder;
using Portfolio.CrossCuttingConcerns.Middleware;

namespace Portfolio.CrossCuttingConcerns;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCrossCuttingMiddleware(this IApplicationBuilder applicationBuilder)
    {
        //applicationBuilder.UseMiddleware<RequestResponseLogMiddleware>();

        applicationBuilder.UseMiddleware<ExceptionHandlerMiddleware>();
        applicationBuilder.UseMiddleware<UnauthorizedMiddleware>();

        return applicationBuilder;
        
    }
}