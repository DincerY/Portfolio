using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Application.Features.Category.Requests;
using Portfolio.Application.Interfaces;

namespace Portfolio.Application.Features.Category.Handlers;

public class GetCategoryRequestHandler : IRequestHandler<GetCategoryRequest,CategoryDTO>
{
    private readonly ICategoryService _categoryService;

    public GetCategoryRequestHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<CategoryDTO> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
    {
         return _categoryService.GetCategoryById(request.Id);
    }
}