using MediatR;

namespace Portfolio.Application.Features.User.DeleteUser;

public class DeleteUserRequest : IRequest<DeleteUserResponse>
{
    public string Username { get; set; }
}