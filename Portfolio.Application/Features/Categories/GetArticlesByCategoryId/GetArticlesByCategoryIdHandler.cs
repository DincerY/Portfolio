using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Categories.GetArticlesByCategoryId;

public class GetArticlesByCategoryIdHandler : IRequestHandler<GetArticlesByCategoryIdRequest,IEnumerable<GetArticlesByCategoryIdResponse>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetArticlesByCategoryIdHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    //TODO : Burası ya article altında olmamalı yada başka bir çözüm getirilmeli çünkü bu handleri category controller kullanıyor bu yaklaşım doğru mu bilmiyorum buna bakacağım.
    public async Task<IEnumerable<GetArticlesByCategoryIdResponse>> Handle(GetArticlesByCategoryIdRequest request, CancellationToken cancellationToken)
    {
        var category = _categoryRepository.GetByIdWithRelation(request.Id, cat => cat.Articles);
        if (category == null)
        {
            throw new NotFoundException("There is no category in the entered id");
        }

        //return _mapper.Map<IEnumerable<ArticlesWithCategoryDTO>>(category);

        return category.Articles.Select(art => new GetArticlesByCategoryIdResponse()
        {
            Title = art.Title,
            Content = art.Content,
            Name = art.Name,
            CategoryName = category.Name,
            CategoryDescription = category.Description
        }).ToList();
    }
}