using MediatR;


namespace Portfolio.Application.Features.Articles.GetArticlesByCategoryId;

public class GetArticlesByCategoryIdRequest : IRequest<IEnumerable<GetArticlesByCategoryIdResponse>>
{
    public int Id { get; set; }
}