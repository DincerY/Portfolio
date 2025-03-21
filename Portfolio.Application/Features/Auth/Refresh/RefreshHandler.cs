using MediatR;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.CrossCuttingConcerns.Exceptions;

namespace Portfolio.Application.Features.Auth.Refresh;

public class RefreshHandler : IRequestHandler<RefreshRequest,RefreshResponse>
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public RefreshHandler(ITokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<RefreshResponse> Handle(RefreshRequest request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetByRefreshToken(request.RefreshToken);

        if (user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            throw new NotFoundException("User or Refresh token is not exist");
        }

        if (!_tokenService.TokenIsValid(request.Token))
        {
            throw new SecurityTokenException("Token is invalid");
        }

        if (!_tokenService.TokenIsExpired(request.Token))
        {
            throw new SecurityTokenException("Token is not expired");
        }

        var jwtModel = _tokenService.GenerateToken(user);

        user.RefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow;
        _userRepository.Update(user);

        return new RefreshResponse()
        {
            Token = jwtModel.Token,
            RefreshToken = user.RefreshToken,
            AccessTokenExpireDate = DateTime.Now,
            Role = jwtModel.Role,
            Username = jwtModel.Username
        };

    }
}