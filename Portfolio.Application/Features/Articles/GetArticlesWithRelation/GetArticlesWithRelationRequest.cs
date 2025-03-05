using MediatR;

namespace Portfolio.Application.Features.Articles.GetArticlesWithRelation;

public class GetArticlesWithRelationRequest : IRequest<IEnumerable<GetArticlesWithRelationResponse>>
{

}