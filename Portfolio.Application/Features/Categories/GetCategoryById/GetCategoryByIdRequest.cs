using MediatR;
using Portfolio.Application.Behaviors.Caching;


namespace Portfolio.Application.Features.Categories.GetCategoryById;

public class GetCategoryByIdRequest : IRequest<GetCategoryByIdResponse> , ICachableRequest
{
    public int Id { get; set; }
    public string CacheKey => $"GetCategory({Id})";
    public string? CacheGroupKey => "GetCategoryById";
    public TimeSpan? SlidingExpiration { get; }
}