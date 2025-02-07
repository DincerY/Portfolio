using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Application.Services;
using Portfolio.Application.Validators;

namespace Portfolio.Application;

public static class ApplicationServiceRegistration 
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection service)
    {
        service.AddScoped<IArticleService, ArticleService>();
        service.AddScoped<IAuthorService, AuthorService>();
        service.AddScoped<ICategoryService, CategoryService>();

        service.AddSingleton<IValidator<CreateCategoryDTO>, CreateCategoryDTOValidator>();
        service.AddSingleton<IValidator<CreateArticleDTO>, CreateArticleDTOValidator>();
        service.AddSingleton<IValidator<CreateAuthorDTO>, CreateAuthorDTOValidator>();

        return service;
    }
}