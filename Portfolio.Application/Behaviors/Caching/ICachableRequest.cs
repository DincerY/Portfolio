
namespace Portfolio.Application.Behaviors.Caching;

public interface ICachableRequest
{
    string CacheKey { get; }
    string? CacheGroupKey { get; }
    TimeSpan? SlidingExpiration { get; }
}