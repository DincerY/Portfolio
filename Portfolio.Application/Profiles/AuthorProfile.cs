using AutoMapper;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Profiles;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, AuthorDTO>().ReverseMap();

        CreateMap<CreateAuthorDTO, Author>()
            .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(o => DateTime.UtcNow));
    }
}