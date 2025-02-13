using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Portfolio.Common.Response;
using Portfolio.CrossCuttingConcerns.Exceptions;

namespace Portfolio.CrossCuttingConcerns.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
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
        var json = JsonConvert.SerializeObject(response);
        await context.Response.WriteAsync(json);
    }
}