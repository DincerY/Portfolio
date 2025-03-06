using MediatR;
using Portfolio.Application.Behaviors.Caching;

namespace Portfolio.Application.Features.Authors.GetAuthorById;

public class GetAuthorByIdRequest : IRequest<GetAuthorByIdResponse>, ICachableRequest
{
    public int Id { get; set; }
    public string CacheKey => $"GetAuthor({Id})";
    public string? CacheGroupKey => "GetAuthorById";
    public TimeSpan? SlidingExpiration { get; }
}