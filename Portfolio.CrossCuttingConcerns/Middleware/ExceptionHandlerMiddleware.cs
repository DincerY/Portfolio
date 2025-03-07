using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Portfolio.Common.Response;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.CrossCuttingConcerns.Logging;
using Portfolio.CrossCuttingConcerns.Logging.Serilog;


namespace Portfolio.CrossCuttingConcerns.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly LoggerServiceBase _loggerServiceBase;
    public ExceptionHandlerMiddleware(RequestDelegate next, LoggerServiceBase loggerServiceBase)
    {
        _next = next;
        _loggerServiceBase = loggerServiceBase;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
            //Bunu aşağı alma sebebim exception işlendikten sonra response tarafının 
            //status kodu vs dolmuş oluyor daha sonra log basıyoruz
            await LogException(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ApiResponse<object> response = new ApiResponse<object>();

        context.Response.ContentType = MediaTypeNames.Application.Json;
        switch (exception)
        {
            case NotFoundException notFoundException:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                response.StatusCode = StatusCodes.Status404NotFound;
                response.Message = notFoundException.Message;
                break;
            case BusinessException businessException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = businessException.Message;
                break;
            case ValidationException validationException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.ValidationErrors = validationException.Errors;
                response.Message = validationException.Message;
                break;
            case BadRequestException badRequestException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = badRequestException.Message;
                break;
            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = exception.Message;
                break;
        }
        var json = JsonConvert.SerializeObject(response,new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize
        });
        await context.Response.WriteAsync(json);
    }

    private Task LogException(HttpContext context, Exception exception)
    {
        var path = context.Request.Path.Value;
        var controller = path.Split('/')[2];

        LogDetailWithException logDetailWithException = new()
        {
            User = context.User.Identity?.Name ?? "?",
            HttpMethod = context.Request.Method,
            QueryParams = context.Request.Query.ToDictionary(q => q.Key, q => q.Value.ToString()),
            UserAgent = context.Request.Headers["User-Agent"].ToString(),
            StatusCode = context.Response.StatusCode,
            TraceId = context.TraceIdentifier,
            Path = path,
            Controller = controller,
            ExceptionMessage = exception.Message,
        };
        _loggerServiceBase.Error(JsonConvert.SerializeObject(logDetailWithException));

        return Task.CompletedTask;
    }
}