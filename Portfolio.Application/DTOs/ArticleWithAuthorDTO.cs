namespace Portfolio.Application.DTOs;

public class ArticleWithAuthorDTO
{
    public string Title { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public List<AuthorDTO> Authors { get; set; }
}