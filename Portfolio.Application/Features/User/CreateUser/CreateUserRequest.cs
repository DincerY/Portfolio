using MediatR;

namespace Portfolio.Application.Features.User.CreateUser;

public class CreateUserRequest : IRequest<CreateUserResponse>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}