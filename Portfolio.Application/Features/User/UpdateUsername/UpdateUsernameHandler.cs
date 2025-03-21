using AutoMapper;
using MediatR;
using Portfolio.Application.Features.User.CreateUser;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.CrossCuttingConcerns.Exceptions;

namespace Portfolio.Application.Features.User.UpdateUser;

public class UpdateUsernameHandler : IRequestHandler<UpdateUsernameRequest,UpdateUsernameResponse>
{
    private readonly IUserRepository _userRepository;

    public UpdateUsernameHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UpdateUsernameResponse> Handle(UpdateUsernameRequest request, CancellationToken cancellationToken)
    {
        //Business Rule

        var existUser =_userRepository.GetByUsername(request.PrevUsername);
        if (existUser == null)
        {
            throw new NotFoundException("User name is not exist");
        }

        var user = _userRepository.GetByUsername(request.NewUsername);
        if (user != null)
        {
            throw new BusinessException("New User name is already exist");
        }

        existUser.Username = request.NewUsername;
        var updatedUser = _userRepository.Update(existUser);

        if (updatedUser != null)
        {
            return new UpdateUsernameResponse()
            {
                Success = true,
                Username = updatedUser.Username
            };
        }
        else
        {
            return new UpdateUsernameResponse()
            {
                Success = false,
                Username = ""
            };
        }
    }
}