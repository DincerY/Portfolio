using MediatR;

namespace Portfolio.Application.Features.Categories.GetCategoriesByArticleId;

public class GetCategoriesByArticleIdRequest : IRequest<IEnumerable<GetCategoriesByArticleIdResponse>>
{
    public int Id { get; set; }
}