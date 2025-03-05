using AutoMapper;
using Portfolio.Application.DTOs;
using Portfolio.Application.Features.Articles.GetArticleById;
using Portfolio.Application.Features.Articles.GetArticles;
using Portfolio.Application.Features.Articles.GetArticlesByAuthorId;
using Portfolio.Application.Features.Articles.GetArticlesByCategoryId;
using Portfolio.Application.Features.Articles.GetArticlesByIds;
using Portfolio.Application.Features.Articles.GetArticlesWithRelation;
using Portfolio.Application.Features.Articles.GetArticleWithRelationById;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Features.Articles.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Article, GetArticleByIdResponse>().ReverseMap();
        CreateMap<Article, GetArticlesResponse>().ReverseMap();
        CreateMap<Article, GetArticlesByAuthorIdResponse>().ReverseMap();
        CreateMap<Article, GetArticlesByCategoryIdResponse>().ReverseMap();
        CreateMap<Article, GetArticlesByIdsResponse>().ReverseMap();
        CreateMap<Article, GetArticlesWithRelationResponse>().ReverseMap();
        CreateMap<Article, GetArticleWithRelationByIdResponse>().ReverseMap();

        CreateMap<Author, AuthorDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();

    }
}