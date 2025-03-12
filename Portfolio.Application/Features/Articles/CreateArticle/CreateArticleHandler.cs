using AutoMapper;
using MediatR;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Entities;
using Portfolio.Application.Interfaces.Repositories;

namespace Portfolio.Application.Features.Articles.CreateArticle;

public class CreateArticleHandler : IRequestHandler<CreateArticleRequest,CreateArticleResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAuthorRepository _authorRepository;

    public CreateArticleHandler(IArticleRepository articleRepository, ICategoryRepository categoryRepository, IAuthorRepository authorRepository)
    {
        _articleRepository = articleRepository;
        _categoryRepository = categoryRepository;
        _authorRepository = authorRepository;
    }

    public async Task<CreateArticleResponse> Handle(CreateArticleRequest request, CancellationToken cancellationToken)
    {
        bool isArticleExits = _articleRepository.IsExists(art => art.Name.ToLower() == request.Name.ToLower());
        if (isArticleExits)
        {
            throw new BusinessException("Article's name is already exist");
        }

        isArticleExits = _articleRepository.IsExists(art => art.Title.ToLower() == request.Title.ToLower());
        if (isArticleExits)
        {
            throw new BusinessException("Article's title is already exist");
        }

        var existAuthors = _authorRepository.GetByIds(request.Authors).ToList();
        if (existAuthors.Count != request.Authors.Count)
        {
            throw new NotFoundException("Entered invalid author IDs.");
        }

        var existCategories = _categoryRepository.GetByIds(request.Categories).ToList();
        if (existCategories.Count != request.Categories.Count)
        {
            throw new NotFoundException("Entered invalid categories IDs.");
        }

        Article article = new Article()
        {
            Content = request.Content,
            Name = request.Name,
            PublishedDate = DateTime.UtcNow,
            Title = request.Title,
            Authors = existAuthors,
            Categories = existCategories
        };
        var addedArticle = _articleRepository.Add(article);

        //bir önce ki yaklaşımda ara tabloyu kullanarak ekleme yapıyordum fakat kontrol için zaten verileri
        //verileri getirmek zorundayım getirmişken de ekleme işlemini direkt navigation property 
        //üzerinden yapmayı daha basit buldum.

        if (addedArticle == null)
        {
            throw new BusinessException("Adding is not successful");
        }
        else
        {
            return new CreateArticleResponse()
            {
                Id = addedArticle.Id,
                Name = addedArticle.Name,
                CreatedDate = article.PublishedDate
            };
        }
    }
}