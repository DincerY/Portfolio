using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Portfolio.Common.Response;

namespace Portfolio.API.Filters;

public class ApiResponseFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {

    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            var response = new ApiResponse<object>()
            {
                Success = true,
                Message = "Success",
                StatusCode = StatusCodes.Status200OK,
                Data = objectResult.Value
            };
            context.Result = new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
        else if (context.Result is EmptyResult)
        {
            var response = new ApiResponse<object>()
            {
                Success = true,
                Message = "Success",
                StatusCode = StatusCodes.Status200OK,
                Data = null
            };
            context.Result = new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}