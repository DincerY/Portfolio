using AutoMapper;
using MediatR;
using Portfolio.Application.Interfaces.Repositories;

namespace Portfolio.Application.Features.Categories.GetCategories;

public class GetCategoriesHandler : IRequestHandler<GetCategoriesRequest, IEnumerable<GetCategoriesResponse>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoriesHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetCategoriesResponse>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        var categories = _categoryRepository.GetAllWithPagination(request.PageSize,request.PageNumber).ToList();
        return _mapper.Map<IEnumerable<GetCategoriesResponse>>(categories);
    }
}