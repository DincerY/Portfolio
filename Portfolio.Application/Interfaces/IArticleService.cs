using Portfolio.Application.DTOs;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces;

public interface IArticleService
{
    public IEnumerable<ArticleDTO> GetArticles();
    public IEnumerable<ArticleWithRelationsDTO> GetArticlesWithRelation();

    public ArticleDTO GetArticleById(int id);
    public ArticleWithRelationsDTO GetArticleWithRelationById(int id);

    public List<Article> GetArticlesByIds(List<int> ids);

    public int AddArticle(CreateArticleDTO dto);

    public List<AuthorDTO> GetAuthorsByArticleId(int id);

}