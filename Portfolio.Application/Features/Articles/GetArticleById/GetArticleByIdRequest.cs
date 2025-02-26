using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Articles.GetArticleById;

public class GetArticleByIdRequest : IRequest<ArticleDTO>
{
    public int Id { get; set; }
}