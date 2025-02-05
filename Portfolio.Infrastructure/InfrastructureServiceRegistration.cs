using Microsoft.Extensions.DependencyInjection;
using Portfolio.Domain.Interfaces.Repositories;
using Portfolio.Infrastructure.Repositories;

namespace Portfolio.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection service)
    {
        service.AddScoped<IArticleRepository, ArticleRepository>();
        service.AddScoped<IAuthorRepository, AuthorRepository>();
        service.AddScoped<ICategoryRepository, CategoryRepository>();

        return service;
    }
}