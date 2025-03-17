using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;



namespace Portfolio.CrossCuttingConcerns.Middleware;
//Middleware kullandığımızda yetkilendirme işlemi her istek için kullanılıyor
//Biz böyle bir şey istemiyoruz. Örnek olarak kullanıcı girişi yaparken
//bir yetkilendirme işlemi yapılmıyor.
public class CustomAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public CustomAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var a = context.Features.Get<EndPoint>();
        var user = context.User;

        var userRole = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        if (userRole != "Admin")
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Forbidden");
            return;
        }
        await _next(context);
    }
}