using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWT.Algorithms;
using JWT.Builder;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Application.Interfaces;

namespace Portfolio.Application.Features.Auth.Login;

public class LoginHandler : IRequestHandler<LoginRequest,LoginResponse>
{
    private readonly ITokenService _tokenService;

    public LoginHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }
    //TODO
    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        if (request.Username != "test" && request.Password != "password")
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }


        var token = _tokenService.GenerateToken(request.Username);
        //TODO
        return new LoginResponse()
        {
            Token = token,
            AuthenticateResult = true,
            //TODO
            AccessTokenExpireDate = tokenDescriptor.Expires ?? DateTime.UtcNow,
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