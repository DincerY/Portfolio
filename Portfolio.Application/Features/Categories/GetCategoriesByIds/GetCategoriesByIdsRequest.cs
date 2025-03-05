using MediatR;

namespace Portfolio.Application.Features.Categories.GetCategoriesByIds;

public class GetCategoriesByIdsRequest : IRequest<IEnumerable<GetCategoriesByIdsResponse>>
{
    public List<int> Ids { get; set; }
}