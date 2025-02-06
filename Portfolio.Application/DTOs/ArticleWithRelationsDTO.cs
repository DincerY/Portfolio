using Portfolio.Domain.Entities;

namespace Portfolio.Application.DTOs;

public class ArticleWithRelationsDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public List<AuthorDTO> Authors { get; set; }
    public List<CategoryDTO> Categories { get; set; }
}