using MediatR;


namespace Portfolio.Application.Features.Articles.GetArticleWithRelationById;

public class GetArticleWithRelationByIdRequest : IRequest<GetArticleWithRelationByIdResponse>
{
    public int Id { get; set; }
}