using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Application.Services;
using Portfolio.Application.Validators;
using Portfolio.CrossCuttingConcerns.Logging.Serilog;
using Portfolio.CrossCuttingConcerns.Logging.Serilog.Logger;

namespace Portfolio.Application;

public static class ApplicationServiceRegistration 
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection service)
    {
        service.AddValidatorsFromAssemblyContaining<CreateArticleDTOValidator>();

        service.AddScoped<IArticleService, ArticleService>();
        service.AddScoped<IAuthorService, AuthorService>();
        service.AddScoped<ICategoryService, CategoryService>();

        /*service.AddSingleton<IValidator<CreateCategoryDTO>, CreateCategoryDTOValidator>();
        service.AddSingleton<IValidator<CreateArticleDTO>, CreateArticleDTOValidator>();
        service.AddSingleton<IValidator<CreateAuthorDTO>, CreateAuthorDTOValidator>();
        service.AddSingleton<IValidator<EntityIdDTO>, EntityIdDTOValidator>();
        service.AddSingleton<IValidator<List<EntityIdDTO>>, EntityIdDTOListValidator>();*/
        


        service.AddSingleton<LoggerServiceBase, FileLogger>();

        return service;
    }
}