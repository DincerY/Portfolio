using AutoMapper;
using Portfolio.Application.Features.Authors.CreateAuthor;
using Portfolio.Application.Features.Authors.GetAuthorById;
using Portfolio.Application.Features.Authors.GetAuthors;
using Portfolio.Application.Features.Authors.GetAuthorsByArticleId;
using Portfolio.Application.Features.Authors.GetAuthorsByIds;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Features.Authors.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<CreateAuthorRequest, Author>()
            .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(o => DateTime.UtcNow));

        CreateMap<Author, GetAuthorByIdResponse>().ReverseMap();
        CreateMap<Author, GetAuthorsResponse>().ReverseMap();
        CreateMap<Author, GetAuthorsByArticleIdResponse>().ReverseMap();
        CreateMap<Author, GetAuthorsByIdsResponse>().ReverseMap();
    }
}