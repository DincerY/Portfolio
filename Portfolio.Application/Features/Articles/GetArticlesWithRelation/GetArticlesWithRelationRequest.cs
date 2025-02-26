using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Articles.GetArticlesWithRelation;

public class GetArticlesWithRelationRequest : IRequest<IEnumerable<ArticleWithRelationsDTO>>
{

}