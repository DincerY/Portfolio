using AutoMapper;
using MediatR;
using Portfolio.Application.Features.Auth.Register;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.CrossCuttingConcerns.Exceptions;

namespace Portfolio.Application.Features.User.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserRequest,CreateUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IHashService _hashService;

    public CreateUserHandler(IUserRepository userRepository, IHashService hashService, IMapper mapper)
    {
        _userRepository = userRepository;
        _hashService = hashService;
        _mapper = mapper;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var existUser = _userRepository.GetByUsername(request.Username);
        if (existUser != null)
        {
            throw new BusinessException("User already exist");
        }

        existUser = _userRepository.GetByEmail(request.Email);
        if (existUser != null)
        {
            throw new BusinessException("Email already used");
        }
        var user = _mapper.Map<Domain.Entities.User>(request);
        user.PasswordHash = _hashService.HashPassword(user.PasswordHash);
        var addedUser = _userRepository.Add(user);
        if (addedUser != null)
        {
            return new CreateUserResponse()
            {
                Success = true,
                Username = addedUser.Username
            };
        }
        else
        {
            return new CreateUserResponse()
            {
                Success = false,
                Username = ""
            };
        }
    }
}