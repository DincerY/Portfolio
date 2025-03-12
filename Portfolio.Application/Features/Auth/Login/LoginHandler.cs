using MediatR;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;

namespace Portfolio.Application.Features.Auth.Login;

public class LoginHandler : IRequestHandler<LoginRequest,LoginResponse>
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;


    public LoginHandler(ITokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }
    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        //TODO : Şifreyi eklerken salt ekleyip hashleyip öyle ekleme yapıcam. Bu işlem register tarafında olucak
        if (request.Username != "test" && request.Password != "password")
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var token = _tokenService.GenerateToken(request.Username);
        return new LoginResponse()
        {
            Token = token.Token,
            AuthenticateResult = true,
            AccessTokenExpireDate = token.Expiration,
            Role = token.Role,
            Username = token.Username,
        };
    }
}