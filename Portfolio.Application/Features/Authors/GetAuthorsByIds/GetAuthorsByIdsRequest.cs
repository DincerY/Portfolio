using MediatR;

namespace Portfolio.Application.Features.Authors.GetAuthorsByIds;

public class GetAuthorsByIdsRequest : IRequest<IEnumerable<GetAuthorsByIdsResponse>>
{
    public List<int> Ids { get; set; }
}