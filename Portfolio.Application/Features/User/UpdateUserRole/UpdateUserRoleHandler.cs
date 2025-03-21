using MediatR;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.CrossCuttingConcerns.Exceptions;

namespace Portfolio.Application.Features.User.UpdateUserRole;

public class UpdateUserRoleHandler : IRequestHandler<UpdateUserRoleRequest,UpdateUserRoleResponse>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserRoleHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UpdateUserRoleResponse> Handle(UpdateUserRoleRequest request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetByUsername(request.Username);
        if (user == null)
        {
            throw new NotFoundException("User is not exist");
        }

        user.Role = request.Role.ToLower();
        _userRepository.Update(user);
        return new UpdateUserRoleResponse()
        {
            Username = user.Username,
            Role = user.Role
        };
    }
}