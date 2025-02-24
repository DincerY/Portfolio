using AutoMapper;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Profiles;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Article, ArticleDTO>().ReverseMap();
        CreateMap<Article, ArticleWithRelationsDTO>().ReverseMap();
        CreateMap<Author, AuthorDTO>().ReverseMap();
    }
}