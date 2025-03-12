using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces.Services;

namespace Portfolio.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public JwtModel GenerateToken(string userName)
    {
        SymmetricSecurityKey symmetricSecurityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

        var tokenHandler = new JwtSecurityTokenHandler();
        //var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, "User")
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            NotBefore = DateTime.UtcNow,
            SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new JwtModel()
        {
            Token = tokenHandler.WriteToken(token),
            Expiration = token.ValidTo,
            Role = tokenDescriptor.Subject.Name,
            Username = userName

        };
    }
}