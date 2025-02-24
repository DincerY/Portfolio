using System.Reflection;
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
        //service.AddFluentValidationAutoValidation();


        service.AddScoped<IArticleService, ArticleService>();
        service.AddScoped<IAuthorService, AuthorService>();
        service.AddScoped<ICategoryService, CategoryService>();


        //Yukarıda bütün validatorları otomatik kayıt ettik
        /*service.AddTransient<IValidator<CreateCategoryDTO>, CreateCategoryDTOValidator>();
        service.AddTransient<IValidator<CreateArticleDTO>, CreateArticleDTOValidator>();
        service.AddTransient<IValidator<CreateAuthorDTO>, CreateAuthorDTOValidator>();
        service.AddTransient<IValidator<EntityIdDTO>, EntityIdDTOValidator>();
        service.AddTransient<IValidator<List<EntityIdDTO>>, EntityIdDTOListValidator>();*/

        service.AddSingleton<LoggerServiceBase, FileLogger>();
        service.AddAutoMapper(Assembly.GetExecutingAssembly());

        return service;
    }
}