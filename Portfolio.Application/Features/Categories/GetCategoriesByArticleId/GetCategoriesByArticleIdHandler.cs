using AutoMapper;
using MediatR;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Entities;
using Portfolio.Application.Interfaces.Repositories;

namespace Portfolio.Application.Features.Categories.GetCategoriesByArticleId;

public class GetCategoriesByArticleIdHandler : IRequestHandler<GetCategoriesByArticleIdRequest,IEnumerable<GetCategoriesByArticleIdResponse>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoriesByArticleIdHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetCategoriesByArticleIdResponse>> Handle(GetCategoriesByArticleIdRequest request, CancellationToken cancellationToken)
    {
        var categories = _categoryRepository.GetWhere(cat =>
            cat.ArticleCategories.Any(ac => ac.CategoryId == cat.Id && ac.ArticleId == request.Id));
        if (categories == null)
        {
            throw new NotFoundException("There is no categories in the entered id");
        }

        return _mapper.Map<IEnumerable<GetCategoriesByArticleIdResponse>>(categories);
    }
}