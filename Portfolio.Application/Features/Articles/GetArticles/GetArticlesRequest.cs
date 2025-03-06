using MediatR;
using Portfolio.Application.Behaviors.Caching;


namespace Portfolio.Application.Features.Articles.GetArticles;

public class GetArticlesRequest : IRequest<IEnumerable<GetArticlesResponse>>, ICachableRequest
{
    public string CacheKey => "GetArticles";
    public string? CacheGroupKey { get; }
    public TimeSpan? SlidingExpiration { get; }
}