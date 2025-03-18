using MediatR;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.CrossCuttingConcerns.Exceptions;

namespace Portfolio.Application.Features.User.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserRequest,DeleteUserResponse>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetByUsername(request.Username);
        if (user == null)
        {
            throw new NotFoundException("Username is not exist");
        }

        user.IsActive = false;
        _userRepository.Update(user);
        return new DeleteUserResponse()
        {
            Success = true,
            Username = request.Username
        };
    }
}