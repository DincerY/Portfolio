using System.Text;
using System.Text.Json;
using MediatR;
using Portfolio.Application.Interfaces.Services;
using StackExchange.Redis;

namespace Portfolio.Application.Behaviors.Caching;

public class CachingBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest,TResponse> where TRequest : ICachableRequest
{
    private readonly ICacheService _cacheService;

    public CachingBehavior(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse? response;

        var cachedResponse = await _cacheService.GetAsync<TResponse>(request.CacheKey);
        if (cachedResponse != null)
        {
            return cachedResponse;
        }
        else
        {
            response = await GetResponseAndAddToCache(request, next);
        }
        return response;
    }

    private async Task<TResponse?> GetResponseAndAddToCache(TRequest request, RequestHandlerDelegate<TResponse> next)
    {
        TResponse response = await next();

        await _cacheService.SetAsync(request.CacheKey, response, TimeSpan.FromMinutes(10));

        if (request.CacheGroupKey != null)
        {
            await AddCacheKeyToGroup(request);
        }

        return response;
    }

    private async Task AddCacheKeyToGroup(TRequest request)
    {
        var cacheGroup = await _cacheService.GetAsync<string>(request.CacheGroupKey);

        string cacheKeysInGroup;

        if (cacheGroup != null)
        {
            List<string> list = cacheGroup.Split(',').ToList();
            if (!list.Contains(request.CacheKey))
            {
                list.Add(request.CacheKey);
            }
            var stringBuilder = new StringBuilder();
            foreach (var str in list)
            {
                stringBuilder.Append(str).Append(",");
            }

            stringBuilder.Remove(stringBuilder.Length-1, 1);
            cacheKeysInGroup = stringBuilder.ToString().Trim();
        }
        else
        {
            cacheKeysInGroup =  request.CacheKey;
        }
        await _cacheService.SetAsync(request.CacheGroupKey, cacheKeysInGroup,TimeSpan.FromMinutes(10));
    }
}
