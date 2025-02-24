using Portfolio.Application.DTOs;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces;

public interface IArticleService
{
    public IEnumerable<ArticleDTO> GetArticles();
    public IEnumerable<ArticleWithRelationsDTO> GetArticlesWithRelation();

    public ArticleDTO GetArticleById(EntityIdDTO dto);
    public ArticleWithRelationsDTO GetArticleWithRelationById(EntityIdDTO dto);

    public IEnumerable<ArticleDTO> GetArticlesByIds(List<EntityIdDTO> dtos);

    public int AddArticle(CreateArticleDTO dto);

    public IEnumerable<AuthorDTO> GetAuthorsByArticleId(EntityIdDTO dto);

}