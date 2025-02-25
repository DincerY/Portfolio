using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Application.Features.Category.Requests;
using Portfolio.Application.Interfaces;
using Portfolio.Application.Services;

namespace Portfolio.Application.Features.Category.Handlers;

public class GetCategoriesRequestHandler : IRequestHandler<GetCategoriesRequest,List<CategoryDTO>>
{
    private readonly ICategoryService _categoryService;

    public GetCategoriesRequestHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<List<CategoryDTO>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        return _categoryService.GetCategories().ToList();
    }
}