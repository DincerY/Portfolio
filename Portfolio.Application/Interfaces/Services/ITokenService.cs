using Microsoft.IdentityModel.Tokens;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Interfaces.Services;

public interface ITokenService
{
    JwtModel GenerateToken(string userName);
}