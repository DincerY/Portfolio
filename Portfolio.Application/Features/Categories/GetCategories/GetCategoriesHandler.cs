using AutoMapper;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Categories.GetCategories;

public class GetCategoriesHandler : IRequestHandler<GetCategoriesRequest, IEnumerable<CategoryDTO>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoriesHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDTO>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        var categories = _categoryRepository.GetAll();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
    }
}