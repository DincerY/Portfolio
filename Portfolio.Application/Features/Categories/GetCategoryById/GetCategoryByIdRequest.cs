using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Categories.GetCategoryById;

public class GetCategoryByIdRequest : IRequest<CategoryDTO>
{
    public int Id { get; set; }
}