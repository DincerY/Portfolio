using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Articles.GetArticlesByIds;

public class GetArticlesByIdsRequest : IRequest<IEnumerable<ArticleDTO>>
{
    public List<int> Ids { get; set; }
}