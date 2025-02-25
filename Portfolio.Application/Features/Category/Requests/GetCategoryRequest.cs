using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Category.Requests;

public class GetCategoryRequest : IRequest<CategoryDTO>
{
    public int Id { get; set; }
}