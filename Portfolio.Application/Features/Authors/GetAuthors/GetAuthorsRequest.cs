using MediatR;
using Portfolio.Application.Behaviors.Caching;
using Portfolio.Application.Common;

namespace Portfolio.Application.Features.Authors.GetAuthors;

public class GetAuthorsRequest : IRequest<IEnumerable<GetAuthorsResponse>>, ICachableRequest,IPaginableRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string CacheKey => $"GetAuthors({PageSize}-{PageNumber})";
    public string? CacheGroupKey => "GetAuthors";
    public TimeSpan? SlidingExpiration { get; }
}