using Microsoft.IdentityModel.Tokens;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces.Services;

public interface ITokenService
{
    JwtModel GenerateToken(User user);
}