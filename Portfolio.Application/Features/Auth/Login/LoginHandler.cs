using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWT.Algorithms;
using JWT.Builder;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Portfolio.Application.Features.Auth.Login;

public class LoginHandler : IRequestHandler<LoginRequest,LoginResponse>
{
    private readonly IConfiguration _configuration;

    public LoginHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    //TODO
    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        if (request.Username != "test" && request.Password != "password")
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, "User")
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        //TODO
        return new LoginResponse()
        {
            Token = tokenHandler.WriteToken(token),
        };
    }

    //TODO
    /*public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        if (request.Username != "test" && request.Password != "password")
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var token = JwtBuilder.Create()
            .WithAlgorithm(new RS256Algorithm(certificate))
            .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
            .AddClaim("claim1", 0)
            .AddClaim("claim2", "claim2-value")
            .Encode();
        //TODO
        return new LoginResponse()
        {
            Token = token
        };
    }*/
}