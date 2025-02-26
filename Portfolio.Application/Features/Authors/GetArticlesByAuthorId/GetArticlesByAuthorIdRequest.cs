using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Authors.GetArticlesByAuthorId;

public class GetArticlesByAuthorIdRequest : IRequest<IEnumerable<ArticleDTO>>
{
    public int Id { get; set; }
}