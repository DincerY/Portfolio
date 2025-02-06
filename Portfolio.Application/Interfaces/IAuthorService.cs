using Portfolio.Application.DTOs;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces;

public interface IAuthorService
{
    public IEnumerable<AuthorDTO> GetAuthors();
    public Author GetAuthorById(int id);
    public List<Author> GetAuthorsByIds(List<int> ids);
    public int AddAuthor(CreateAuthorDTO dto);
    public List<ArticleDTO> GetArticlesByAuthorId(int authorId);


}