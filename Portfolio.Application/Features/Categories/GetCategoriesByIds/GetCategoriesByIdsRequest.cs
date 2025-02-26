using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Categories.GetCategoriesByIds;

public class GetCategoriesByIdsRequest : IRequest<IEnumerable<CategoryDTO>>
{
    public List<int> Ids { get; set; }
}