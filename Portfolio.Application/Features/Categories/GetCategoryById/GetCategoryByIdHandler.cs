using AutoMapper;
using MediatR;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Application.Interfaces.Repositories;

namespace Portfolio.Application.Features.Categories.GetCategoryById;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdRequest, GetCategoryByIdResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryByIdHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
    {
        var category = _categoryRepository.GetById(request.Id);
        if (category == null)
        {
            throw new NotFoundException("There is no category in the entered id");
        }

        return _mapper.Map<GetCategoryByIdResponse>(category);
    }
}