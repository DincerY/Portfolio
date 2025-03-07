using MediatR;
using Portfolio.Application.Behaviors.Caching;

namespace Portfolio.Application.Features.Authors.CreateAuthor;

public class CreateAuthorRequest : IRequest<CreateAuthorResponse> , ICacheRemoverRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? CacheKey { get; }
    public string? CacheGroupKey => "GetAuthors";
}