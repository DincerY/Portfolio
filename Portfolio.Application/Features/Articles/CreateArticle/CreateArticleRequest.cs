using MediatR;
using Portfolio.Application.Behaviors.Caching;

namespace Portfolio.Application.Features.Articles.CreateArticle;

public class CreateArticleRequest : IRequest<CreateArticleResponse>, ICacheRemoverRequest
{
    public string Title { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public List<int> Authors { get; set; }
    public List<int> Categories { get; set; }
    public string? CacheKey { get; }
    public string? CacheGroupKey => "GetArticles";
}