using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Authors.GetAuthorsByIds;

public class GetAuthorsByIdsRequest : IRequest<IEnumerable<AuthorDTO>>
{
    public List<int> Ids { get; set; }
}