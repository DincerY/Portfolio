using MediatR;

namespace Portfolio.Application.Features.User.ActiveUser;

public class ActiveUserRequest : IRequest<ActiveUserResponse>
{
    public string Username { get; set; }
}