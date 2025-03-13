using AutoMapper;
using MediatR;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Features.Auth.Register;

public class RegisterHandler : IRequestHandler<RegisterRequest,RegisterResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;
    private readonly IMapper _mapper;

    public RegisterHandler(IUserRepository userRepository, IMapper mapper, IHashService hashService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _hashService = hashService;
    }

    public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        user.PasswordHash = _hashService.HashPassword(user.PasswordHash);
        var addedUser = _userRepository.Add(user);
        if (addedUser != null)
        {
            return new RegisterResponse()
            {
                Success = true,
                Username = addedUser.Username
            };
        }
        else
        {
            return new RegisterResponse()
            {
                Success = false,
                Username = ""
            };
        }
        
    }
}