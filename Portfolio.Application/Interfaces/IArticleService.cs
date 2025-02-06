using Portfolio.Application.DTOs;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces;

public interface IArticleService
{
    public IEnumerable<ArticleWithRelationsDTO> GetArticles();
    public ArticleWithRelationsDTO GetArticleById(int id);
    public List<Article> GetArticlesByIds(List<int> ids);

    public int AddArticle(CreateArticleDTO dto);

    public List<AuthorDTO> GetAuthorsByArticleId(int id);

}