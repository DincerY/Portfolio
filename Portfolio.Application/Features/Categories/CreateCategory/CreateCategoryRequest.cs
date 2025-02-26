using MediatR;

namespace Portfolio.Application.Features.Categories.CreateCategory;

public class CreateCategoryRequest : IRequest<CreateCategoryResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
}