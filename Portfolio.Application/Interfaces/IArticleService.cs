using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces;

public interface IArticleService
{
    public IEnumerable<Article> GetArticles();
    public Article GetArticleById(int id);
    public int AddArticle(Article entity);

}