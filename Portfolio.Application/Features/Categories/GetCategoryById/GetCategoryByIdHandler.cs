using AutoMapper;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Categories.GetCategoryById;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdRequest,CategoryDTO>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryByIdHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryDTO> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
    {
        var category = _categoryRepository.GetById(request.Id);
        if (category == null)
        {
            throw new NotFoundException("There is no category in the entered id");
        }

        return _mapper.Map<CategoryDTO>(category);
    }
}