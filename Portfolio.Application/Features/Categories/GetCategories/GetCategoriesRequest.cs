using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Categories.GetCategories;

public class GetCategoriesRequest : IRequest<IEnumerable<CategoryDTO>>
{

}