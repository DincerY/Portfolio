using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Category.Requests;

public class GetCategoriesRequest : IRequest<List<CategoryDTO>>
{

}