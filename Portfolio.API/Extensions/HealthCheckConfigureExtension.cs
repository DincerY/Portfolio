using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Portfolio.API.Extensions;

public static class HealthCheckConfigureExtension
{
    public static IApplicationBuilder UseCustomHealthChech(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/api/health", new HealthCheckOptions()
        {
            ResponseWriter = async (context, report) =>
            {
                await context.Response.WriteAsync("OK");
            }
        });
        return app;
    }
}