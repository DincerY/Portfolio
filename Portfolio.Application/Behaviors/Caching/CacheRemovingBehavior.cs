using MediatR;
using Portfolio.Application.Interfaces.Services;

namespace Portfolio.Application.Behaviors.Caching;

public class CacheRemovingBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest,TResponse>  where TRequest : ICacheRemoverRequest
{
    private readonly ICacheService _cacheService;

    public CacheRemovingBehavior(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response = await next();

        if (request.CacheGroupKey != null)
        {
            var cacheGroupKeys = await _cacheService.GetAsync<string>(request.CacheGroupKey);
            if (cacheGroupKeys != null)
            {
                var keys = cacheGroupKeys.Split(",").ToList();
                foreach (var key in keys)
                {
                    await _cacheService.RemoveAsync(key);
                }

                await _cacheService.RemoveAsync(request.CacheGroupKey);
            }
        }

        if (request.CacheKey != null)
        {
            await _cacheService.RemoveAsync(request.CacheKey);
        }
        return response;
    }
}