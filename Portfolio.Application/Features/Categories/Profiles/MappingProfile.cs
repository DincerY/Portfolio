using AutoMapper;
using Portfolio.Application.Features.Categories.CreateCategory;
using Portfolio.Application.Features.Categories.GetCategories;
using Portfolio.Application.Features.Categories.GetCategoriesByArticleId;
using Portfolio.Application.Features.Categories.GetCategoriesByIds;
using Portfolio.Application.Features.Categories.GetCategoryById;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Features.Categories.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Tek yönlü
        CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<Category, GetCategoriesByArticleIdResponse>().ReverseMap();
        CreateMap<Category, GetCategoriesResponse>().ReverseMap();
        CreateMap<Category, GetCategoriesByIdsResponse>().ReverseMap();
        CreateMap<Category, GetCategoryByIdResponse>().ReverseMap();
        CreateMap<Category, CreateCategoryResponse>().ReverseMap();
    }
}