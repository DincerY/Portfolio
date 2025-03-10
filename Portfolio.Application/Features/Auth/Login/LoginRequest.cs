
using MediatR;

namespace Portfolio.Application.Features.Auth.Login;

public class LoginRequest : IRequest<LoginResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }

}