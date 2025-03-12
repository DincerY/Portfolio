using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Interfaces.Services;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Infrastructure.Repositories;
using Portfolio.Infrastructure.Services;

namespace Portfolio.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection service)
    {
        service.AddScoped<IArticleRepository, ArticleRepository>();
        service.AddScoped<IAuthorRepository, AuthorRepository>();
        service.AddScoped<ICategoryRepository, CategoryRepository>();
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddSingleton<ICacheService, RedisCacheService>();
        service.AddScoped<ITokenService, TokenService>();
        return service;
    }
}