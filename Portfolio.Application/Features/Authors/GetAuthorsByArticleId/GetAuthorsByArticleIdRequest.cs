using MediatR;

namespace Portfolio.Application.Features.Authors.GetAuthorsByArticleId;

public class GetAuthorsByArticleIdRequest : IRequest<IEnumerable<GetAuthorsByArticleIdResponse>>
{
    public int Id { get; set; }
}