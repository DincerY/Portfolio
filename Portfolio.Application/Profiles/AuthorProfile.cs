using AutoMapper;
using Portfolio.Application.DTOs;
using Portfolio.Application.Features.Authors.CreateAuthor;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Profiles;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, AuthorDTO>().ReverseMap();

        CreateMap<CreateAuthorRequest, Author>()
            .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(o => DateTime.UtcNow));
    }
}