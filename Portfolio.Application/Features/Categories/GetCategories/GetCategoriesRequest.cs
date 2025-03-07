using MediatR;
using Portfolio.Application.Behaviors.Caching;
using Portfolio.Application.Common;

namespace Portfolio.Application.Features.Categories.GetCategories;

public class GetCategoriesRequest : IRequest<IEnumerable<GetCategoriesResponse>>, ICachableRequest, IPaginableRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public string CacheKey => $"GetCategories({PageSize}-{PageNumber})";
    public string? CacheGroupKey => "GetCategories";
    public TimeSpan? SlidingExpiration { get; }

}