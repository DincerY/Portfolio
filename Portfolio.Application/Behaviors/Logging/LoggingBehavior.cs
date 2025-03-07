using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Portfolio.CrossCuttingConcerns.Logging;
using Portfolio.CrossCuttingConcerns.Logging.Serilog;

namespace Portfolio.Application.Behaviors.Logging;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly LoggerServiceBase _loggerService;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public LoggingBehavior(LoggerServiceBase loggerService, IHttpContextAccessor httpContextAccessor)
    {
        _loggerService = loggerService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _loggerService.Info("Request : " + JsonConvert.SerializeObject(new RequestLogDetail()
        {
            HttpMethod = _httpContextAccessor.HttpContext?.Request.Method,
            ContentType = _httpContextAccessor.HttpContext?.Request.ContentType,
            Path = _httpContextAccessor.HttpContext?.Request.Path.Value,
            TraceId = _httpContextAccessor.HttpContext.TraceIdentifier,
            User = _httpContextAccessor.HttpContext.User.Identity?.Name ?? "?"
        }));

        TResponse response = await next();

        var path = _httpContextAccessor.HttpContext.Request.Path.Value;
        var controller = path.Split('/')[2];

        ResponseLogDetail responseLogDetail = new()
        {
            User = _httpContextAccessor.HttpContext.User.Identity?.Name ?? "?",
            HttpMethod = _httpContextAccessor.HttpContext.Request.Method,
            QueryParams = _httpContextAccessor.HttpContext.Request.Query.ToDictionary(q => q.Key, q => q.Value.ToString()),
            UserAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString(),
            StatusCode = _httpContextAccessor.HttpContext.Response.StatusCode,
            TraceId = _httpContextAccessor.HttpContext.TraceIdentifier,
            Path = path,
            Controller = controller,
            RequestType = request.GetType().Name,
        };
        _loggerService.Info("Response : " + JsonConvert.SerializeObject(responseLogDetail));

        return response;
    }
}