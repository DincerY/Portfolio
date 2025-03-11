using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Behaviors.Caching;
using Portfolio.Application.Behaviors.Logging;
using Portfolio.Application.Behaviors.Validation;
using Portfolio.Application.Features.Articles.CreateArticle;
using Portfolio.Application.Interfaces;
using Portfolio.Application.Services;
using Portfolio.CrossCuttingConcerns.Logging.Serilog;
using Portfolio.CrossCuttingConcerns.Logging.Serilog.Logger;
using StackExchange.Redis;

namespace Portfolio.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection service)
    {
        service.AddValidatorsFromAssemblyContaining<CreateArticleRequestValidator>();

        //Yukarıda bütün validatorları otomatik kayıt ettik
        /*service.AddTransient<IValidator<CreateAuthorRequest>, CreateAuthorRequestValidator>();
        service.AddTransient<IValidator<CreateArticleRequest>, CreateArticleRequestValidator>();
        service.AddTransient<IValidator<CreateCategoryRequest>, CreateCategoryRequestValidator>();*/

        service.AddSingleton<LoggerServiceBase, FileLogger>();
        service.AddAutoMapper(Assembly.GetExecutingAssembly());

        service.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(IdValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
        });
        service.AddSingleton<IDatabase>(sp =>
        {
            var configuration = sp.GetService<IConfiguration>();
            return ConnectionMultiplexer.Connect(configuration["Redis:ConnectionString"]).GetDatabase();
        });

        service.AddSingleton<ICacheService,RedisCacheService>();
        service.AddSingleton<ITokenService,TokenService>();

        return service;
    }
}