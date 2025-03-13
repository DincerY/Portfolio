using MediatR;

namespace Portfolio.Application.Features.Auth.Register;

public class RegisterRequest : IRequest<RegisterResponse>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}