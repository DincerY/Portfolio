using AutoMapper;
using Portfolio.Application.Features.Auth.Register;
using Portfolio.Application.Features.User.CreateUser;
using Portfolio.Application.Features.User.UpdateUser;

namespace Portfolio.Application.Features.User.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserRequest, Domain.Entities.User>()
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(req => req.Password));
    }
}