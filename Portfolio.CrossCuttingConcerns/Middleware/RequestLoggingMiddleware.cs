using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Portfolio.CrossCuttingConcerns.Logging;
using Portfolio.CrossCuttingConcerns.Logging.Serilog;

namespace Portfolio.CrossCuttingConcerns.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly LoggerServiceBase _loggerService;

    public RequestLoggingMiddleware(RequestDelegate next, LoggerServiceBase loggerService)
    {
        _next = next;
        _loggerService = loggerService;
    }

    public async Task Invoke(HttpContext context)
    {
        List<LogParameter> logParameters = new List<LogParameter>()
        {
            new LogParameter() { Type = context.GetType().Name, Value = context }
        };
        LogDetail logDetail = new()
        {
            MethodName = _next.Method.Name,
            Parameters = logParameters,
            //TODO:Authentication ekledikten sonra düzenleyebiliriz.
            User = "?",
            FullName = context.GetType().FullName
        };
        //_loggerService.Info(JsonConvert.SerializeObject(logDetail, new JsonSerializerSettings()
        //{
        //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //}));
        _loggerService.Info("test deneme test");
        await _next(context);
    }
}