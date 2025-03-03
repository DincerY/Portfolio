using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Authors.GetAuthorsByArticleId;

public class GetAuthorsByArticleIdRequest : IRequest<IEnumerable<AuthorDTO>>
{
    public int Id { get; set; }
}