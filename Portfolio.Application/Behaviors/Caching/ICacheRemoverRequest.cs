namespace Portfolio.Application.Behaviors.Caching;

public interface ICacheRemoverRequest
{
    string? CacheKey { get; }
    string? CacheGroupKey { get; }
}