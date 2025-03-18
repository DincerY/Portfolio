using MediatR;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.CrossCuttingConcerns.Exceptions;

namespace Portfolio.Application.Features.User.ActiveUser;

public class ActiveUserHandler : IRequestHandler<ActiveUserRequest,ActiveUserResponse>
{
    private readonly IUserRepository _userRepository;

    public ActiveUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ActiveUserResponse> Handle(ActiveUserRequest request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetByUsername(request.Username);
        if (user == null)
        {
            throw new NotFoundException("Username is not exist");
        }
        user.IsActive = true;
        _userRepository.Update(user);
        return new ActiveUserResponse()
        {
            Success = true,
            Username = user.Username
        };
    }
}