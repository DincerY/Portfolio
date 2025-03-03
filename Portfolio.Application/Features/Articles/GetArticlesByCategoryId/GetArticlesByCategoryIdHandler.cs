using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Articles.GetArticlesByCategoryId;

public class GetArticlesByCategoryIdHandler : IRequestHandler<GetArticlesByCategoryIdRequest,IEnumerable<GetArticlesByCategoryIdResponse>>
{
    //TODO : Bu kısıma bakıcam
    private readonly IArticleRepository _articleRepository;
    private readonly ICategoryRepository _categoryRepository;

    public GetArticlesByCategoryIdHandler(IArticleRepository articleRepository, ICategoryRepository categoryRepository)
    {
        _articleRepository = articleRepository;
        _categoryRepository = categoryRepository;
    }

    //TODO : Burası ya article altında olmamalı yada başka bir çözüm getirilmeli çünkü bu handleri category controller kullanıyor bu yaklaşım doğru mu bilmiyorum buna bakacağım.
    public async Task<IEnumerable<GetArticlesByCategoryIdResponse>> Handle(GetArticlesByCategoryIdRequest request, CancellationToken cancellationToken)
    {
        var articles = _articleRepository.GetByIdWithRelation(request.Id, art => art.Categories);
        if (articles == null)
        {
            throw new NotFoundException("There is no category in the entered id");
        }

        return articles.Select(art => new GetArticlesByCategoryIdResponse()
        {
            Title = art.Title,
            Content = art.Content,
            Name = art.Name,
            CategoryName = art.Categories.First(ac => ac.Id == request.Id).Name,
            CategoryDescription = art.Categories.First(ac => ac.Id == request.Id).Name,
        }).ToList();
    }
}