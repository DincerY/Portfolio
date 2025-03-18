using AutoMapper;
using MediatR;
using Portfolio.Application.Features.User.CreateUser;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.CrossCuttingConcerns.Exceptions;

namespace Portfolio.Application.Features.User.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserRequest,UpdateUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IHashService _hashService;

    public UpdateUserHandler(IUserRepository userRepository, IMapper mapper, IHashService hashService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _hashService = hashService;
    }

    public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        //Business Rule

        var existUser =_userRepository.GetByUsername(request.PrevUsername);
        if (existUser == null)
        {
            throw new NotFoundException("User name is not exist");
        }

        var usernames = _userRepository.set.Select(u => u.Username).ToList();
        if (usernames.Contains(request.NewUsername))
        {
            throw new BusinessException("New User name is already exist");
        }

        existUser.Username = request.NewUsername;
        existUser.PasswordHash = _hashService.HashPassword(request.Password);
        existUser.Role = request.Role;

        var updatedUser = _userRepository.Update(existUser);

        if (updatedUser != null)
        {
            return new UpdateUserResponse()
            {
                Success = true,
                Username = updatedUser.Username
            };
        }
        else
        {
            return new UpdateUserResponse()
            {
                Success = false,
                Username = ""
            };
        }
    }
}