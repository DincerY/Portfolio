using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace Portfolio.CrossCuttingConcerns.Middleware;

public class CustomAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public CustomAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }


    public async Task Invoke(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: Token is missing or invalid.");
            return;
        }

        var token = authorizationHeader.Substring("Bearer ".Length).Trim();

        //Token doğrulama
        //--------------------------

        var identity = new ClaimsIdentity(GetJwtClaims(token),"custom_auth");

        context.User = new ClaimsPrincipal(identity);
        //context.User.Identity = true;
        await _next(context);
    }

    private IEnumerable<Claim> GetJwtClaims(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        try
        {
            tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        }
        catch (Exception e)
        {
            throw new SecurityTokenException("Jwt is not valid");
        }
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        return jwtSecurityToken.Claims;
    }
}