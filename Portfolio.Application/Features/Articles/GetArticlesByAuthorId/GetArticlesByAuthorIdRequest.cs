using MediatR;

namespace Portfolio.Application.Features.Articles.GetArticlesByAuthorId;

public class GetArticlesByAuthorIdRequest : IRequest<IEnumerable<GetArticlesByAuthorIdResponse>>
{
    public int Id { get; set; }
}