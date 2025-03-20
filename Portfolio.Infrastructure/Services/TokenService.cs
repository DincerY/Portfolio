using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces.Services;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public JwtModel GenerateToken(User user)
    {
        var token = CreateToken(user);
        
        return new JwtModel()
        {
            Token = token,
            Expiration = DateTime.UtcNow,
            Role = user.Role,
            Username = user.Username
        };
    }

    //TODO : Bu fonksiyonların amacı sedece refresh token üretmek veya doğrulamak veri tabanına kayıt vs işleri bu fonksiyonların içinde bulunmamalıveri tabanına kayıt vs işleri bu fonksiyonların içinde bulunmamalı
    //TODO : Normal Jwt süresi bitmediyse yenisini vermeyeceğim
    public JwtModel GenerateTokenByRefreshToken(string token,User user)
    {
        if (!TokenIsValid(token))
        {
            throw new SecurityTokenException("Token is invalid");
        }
        var newToken = CreateToken(user);
        
        return new JwtModel()
        {
            Token = newToken,
            Expiration = DateTime.UtcNow,
            Role = user.Role,
            Username = user.Username
        };
    }


    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        rng.Dispose();
        return Convert.ToBase64String(randomNumber);
    }

    private string CreateToken(User user)
    {
        SymmetricSecurityKey symmetricSecurityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            NotBefore = DateTime.UtcNow,
            SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private bool TokenIsValid(string token)
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
        tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null)
        {
            return false;
        }
        return true;
    }

}