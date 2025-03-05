using MediatR;

namespace Portfolio.Application.Features.Categories.GetCategories;

public class GetCategoriesRequest : IRequest<IEnumerable<GetCategoriesResponse>>
{

}