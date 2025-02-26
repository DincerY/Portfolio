using AutoMapper;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Categories.GetCategoriesByIds;

public class GetCategoriesByIdsHandler : IRequestHandler<GetCategoriesByIdsRequest,IEnumerable<CategoryDTO>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoriesByIdsHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDTO>> Handle(GetCategoriesByIdsRequest request, CancellationToken cancellationToken)
    {
        var categories = _categoryRepository.GetByIds(request.Ids);
        if (categories.Count() != request.Ids.Count)
        {
            throw new NotFoundException("There is no category in the entered ids");
        }
        return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
    }
}