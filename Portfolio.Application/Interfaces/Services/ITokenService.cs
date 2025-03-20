using Microsoft.IdentityModel.Tokens;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces.Services;

public interface ITokenService
{
    public string GenerateRefreshToken();

    JwtModel GenerateToken(User user);
    public JwtModel GenerateTokenByRefreshToken(string token, User user);

}