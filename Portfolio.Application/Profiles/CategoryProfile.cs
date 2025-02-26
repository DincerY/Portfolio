using AutoMapper;
using Portfolio.Application.DTOs;
using Portfolio.Application.Features.Categories.CreateCategory;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        //Tek yönlü
        CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        /*CreateMap<Category, ArticlesWithCategoryDTO>()
            .ForMember(dest => dest.ArticleDtos, opt => opt.MapFrom(o => o.Articles))
            .ForMember(dest => dest.Description,opt => opt.MapFrom(o => o.Description))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(o => o.Name));*/


    }
}