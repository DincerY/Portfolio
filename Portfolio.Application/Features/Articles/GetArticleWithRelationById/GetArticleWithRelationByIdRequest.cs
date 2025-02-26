using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Articles.GetArticleWithRelationById;

public class GetArticleWithRelationByIdRequest : IRequest<ArticleWithRelationsDTO>
{
    public int Id { get; set; }
}