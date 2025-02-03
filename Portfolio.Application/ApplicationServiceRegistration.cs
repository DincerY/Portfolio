using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Interfaces;
using Portfolio.Application.Services;

namespace Portfolio.Application;

public static class ApplicationServiceRegistration 
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection service)
    {
        service.AddScoped<IArticleService, ArticleService>();
        return service;
    }
}