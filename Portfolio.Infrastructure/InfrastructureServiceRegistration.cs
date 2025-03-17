using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Interfaces.Services;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Infrastructure.Repositories;
using Portfolio.Infrastructure.Services;
using Serilog;
using StackExchange.Redis;

namespace Portfolio.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection service,IConfiguration _configuration, ILogger _logger)
    {
        service.AddScoped<IArticleRepository, ArticleRepository>();
        service.AddScoped<IAuthorRepository, AuthorRepository>();
        service.AddScoped<ICategoryRepository, CategoryRepository>();
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<ITokenService, TokenService>();
        service.AddScoped<IHashService, HashService>();

        try
        {
            var _database = ConnectionMultiplexer.Connect(_configuration["Redis:ConnectionString"], opt =>
            {
                opt.ConnectTimeout = 1000;
            }).GetDatabase();
            service.AddSingleton<IDatabase>(_database);
            service.AddSingleton<ICacheService, RedisCacheService>();
            _logger.Information("RedisCache service available");
        }
        catch (Exception e)
        {
            service.AddSingleton<ICacheService,InMemoryCacheService>();
            _logger.Information("InMemoryCache service available");
        }

        return service;
    }
}