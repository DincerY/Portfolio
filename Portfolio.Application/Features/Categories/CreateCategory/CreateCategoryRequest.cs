using MediatR;
using Portfolio.Application.Behaviors.Caching;

namespace Portfolio.Application.Features.Categories.CreateCategory;

public class CreateCategoryRequest : IRequest<CreateCategoryResponse> , ICacheRemoverRequest
{
    public string Name { get; set; }
    public string Description { get; set; }

    public string? CacheKey { get; }
    public string? CacheGroupKey => "GetCategories";
}