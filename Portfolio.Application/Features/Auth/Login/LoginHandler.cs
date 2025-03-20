using MediatR;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.CrossCuttingConcerns.Exceptions;

namespace Portfolio.Application.Features.Auth.Login;

public class LoginHandler : IRequestHandler<LoginRequest,LoginResponse>
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;


    public LoginHandler(ITokenService tokenService, IUserRepository userRepository, IHashService hashService)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _hashService = hashService;
    }
    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetByUsername(request.Username);
        if (!user.IsActive)
        {
            throw new NotFoundException("Password or username is not correct");
        }
        if (user == null)
        {
            throw new NotFoundException("Password or username is not correct");
        }

        if (!_hashService.VerifyPassword(request.Password,user.PasswordHash))
        {
            throw new NotFoundException("Password or username is not correct");
        }
        
        var token = _tokenService.GenerateToken(user);

        var refreshToken = _tokenService.GenerateRefreshToken();
        var updateUser = _userRepository.GetByUsername(user.Username);
        updateUser.RefreshToken = refreshToken;
        updateUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        _userRepository.Update(updateUser);

        return new LoginResponse()
        {
            Token = token.Token,
            RefreshToken = refreshToken,
            AuthenticateResult = true,
            AccessTokenExpireDate = DateTime.Now,
            Role = token.Role,
            Username = token.Username,
        };
    }
}