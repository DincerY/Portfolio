using Portfolio.Application.DTOs;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces;

public interface IAuthorService
{
    public IEnumerable<AuthorDTO> GetAuthors();
    public Author GetAuthorById(EntityIdDTO dto);
    public List<AuthorDTO> GetAuthorsByIds(List<EntityIdDTO> dtos);
    public int AddAuthor(CreateAuthorDTO dto);
    public List<ArticleDTO> GetArticlesByAuthorId(EntityIdDTO dto);


}