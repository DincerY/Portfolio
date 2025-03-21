using MediatR;

namespace Portfolio.Application.Features.User.UpdateUserRole;

public class UpdateUserRoleRequest : IRequest<UpdateUserRoleResponse>   
{
    public string Username { get; set; }
    public string Role { get; set; }
}