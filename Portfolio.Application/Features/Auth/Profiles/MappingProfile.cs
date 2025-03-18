using AutoMapper;
using Portfolio.Application.Features.Auth.Register;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Features.Auth.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequest, Domain.Entities.User>()
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(req => req.Password));
    }
}