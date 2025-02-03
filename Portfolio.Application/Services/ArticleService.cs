using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articlesRepository)
    {
        _articleRepository = articlesRepository;
    }


    public IEnumerable<Article> GetArticles()
    {
        return _articleRepository.GetAll();
    }

    public Article GetArticleById(int id)
    {
        //business logic devam edecek
        if (id <= 0)
        {
            throw new Exception("Id can not be less then equal 0");
        }
        return _articleRepository.GetById(id);
    }

    public int AddArticle(Article entity)
    {
        return _articleRepository.Add(entity);

    }
}