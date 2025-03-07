using MediatR;
using Portfolio.Application.Behaviors.Caching;
using Portfolio.Application.Common;


namespace Portfolio.Application.Features.Articles.GetArticles;

public class GetArticlesRequest : IRequest<IEnumerable<GetArticlesResponse>>, ICachableRequest , IPaginableRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public string CacheKey => $"GetArticles({PageSize}-{PageNumber})";
    public string? CacheGroupKey => "GetArticles";
    public TimeSpan? SlidingExpiration { get; }
   
}