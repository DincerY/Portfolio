using MediatR;


namespace Portfolio.Application.Features.Articles.GetArticlesByIds;

public class GetArticlesByIdsRequest : IRequest<IEnumerable<GetArticlesByIdsResponse>>
{
    public List<int> Ids { get; set; }
}