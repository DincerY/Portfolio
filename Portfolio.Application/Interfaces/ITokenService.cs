using Microsoft.IdentityModel.Tokens;

namespace Portfolio.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(string userName);
    //TODO
    SecurityToken generateSecurityToken(string userName);
}