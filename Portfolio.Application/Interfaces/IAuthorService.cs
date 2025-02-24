using Portfolio.Application.DTOs;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces;

public interface IAuthorService
{
    public IEnumerable<AuthorDTO> GetAuthors();
    public AuthorDTO GetAuthorById(EntityIdDTO dto);
    public IEnumerable<AuthorDTO> GetAuthorsByIds(List<EntityIdDTO> dtos);
    public int AddAuthor(CreateAuthorDTO dto);
    public IEnumerable<ArticleDTO> GetArticlesByAuthorId(EntityIdDTO dto);


}