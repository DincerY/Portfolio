using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Portfolio.CrossCuttingConcerns.Logging;
using Portfolio.CrossCuttingConcerns.Logging.Serilog;

namespace Portfolio.CrossCuttingConcerns.Middleware;

public class RequestResponseMiddleware
{
    private readonly RequestDelegate _next;
    private readonly LoggerServiceBase _loggerService;

    public RequestResponseMiddleware(RequestDelegate next, LoggerServiceBase loggerService)
    {
        _next = next;
        _loggerService = loggerService;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next.Invoke(context);

        var path = context.Request.Path.Value;
        var controller = path.Split('/')[2];

        RequestResponseLogDetail logDetail = new()
        {
            User = context.User.Identity?.Name ?? "?",
            HttpMethod = context.Request.Method,
            QueryParams = context.Request.Query.ToDictionary(q => q.Key, q=> q.Value.ToString()),
            UserAgent = context.Request.Headers["User-Agent"].ToString(),
            StatusCode = context.Response.StatusCode,
            TraceId = context.TraceIdentifier,
            Path = path,
            Controller = controller
        };
        /*if (context.Response.ContentLength > 0 && context.Response.Body.CanSeek)
        {
            var originalBodyStream = context.Response.Body;
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            String responseText = await new StreamReader(context.Response.Body, Encoding.UTF8).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await context.Response.Body.CopyToAsync(originalBodyStream);
        }*/

        _loggerService.Info(JsonConvert.SerializeObject(logDetail));
    }
}