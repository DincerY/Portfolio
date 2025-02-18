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
            await LogException(context, ex);
            await HandleExceptionAsync(context, ex);
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
        List<LogParameter> logParameters = new List<LogParameter>()
        {
            new LogParameter() { Type = context.GetType().Name, Value = exception.ToString() }
        };
        LogDetailWithException logDetailWithException = new()
        {
            ExceptionMessage = exception.Message,
            MethodName = _next.Method.Name,
            Parameters = logParameters,
            //TODO:Authentication ekledikten sonra düzenleyebiliriz.
            User = "?",
            FullName = context.GetType().FullName
        };
        _loggerServiceBase.Error(JsonConvert.SerializeObject(logDetailWithException));

        return Task.CompletedTask;
    }
}