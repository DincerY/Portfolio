using MediatR;
using Portfolio.Application.Behaviors.Caching;

namespace Portfolio.Application.Features.Articles.GetArticleById;

public class GetArticleByIdRequest : IRequest<GetArticleByIdResponse>, ICachableRequest
{
    public int Id { get; set; }
    public string CacheKey => $"GetArticle({Id})";
    public string? CacheGroupKey => "GetArticleById";
    public TimeSpan? SlidingExpiration { get; }
}