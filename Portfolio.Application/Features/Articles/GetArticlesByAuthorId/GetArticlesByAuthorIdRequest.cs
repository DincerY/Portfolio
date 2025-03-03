using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Articles.GetArticlesByAuthorId;

public class GetArticlesByAuthorIdRequest : IRequest<IEnumerable<ArticleDTO>>
{
    public int Id { get; set; }
}