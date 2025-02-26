using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Behaviors;
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

        //Yukarıda bütün validatorları otomatik kayıt ettik
        /*service.AddTransient<IValidator<CreateCategoryDTO>, CreateCategoryDTOValidator>();
        service.AddTransient<IValidator<CreateArticleDTO>, CreateArticleDTOValidator>();
        service.AddTransient<IValidator<CreateAuthorDTO>, CreateAuthorDTOValidator>();
        service.AddTransient<IValidator<EntityIdDTO>, EntityIdDTOValidator>();
        service.AddTransient<IValidator<List<EntityIdDTO>>, EntityIdDTOListValidator>();*/

        service.AddSingleton<LoggerServiceBase, FileLogger>();
        service.AddAutoMapper(Assembly.GetExecutingAssembly());

        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return service;
    }
}