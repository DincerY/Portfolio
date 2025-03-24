using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Portfolio.API.Filters;

public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _role;

    public CustomAuthorizeAttribute(string role)
    {
        _role = role;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (!user.Identity?.IsAuthenticated ?? false)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var roleClaim = user.FindFirst(ClaimTypes.Role)?.Value;
        if (roleClaim != _role)
        {
            context.Result = new ForbidResult();
        }
    }
}