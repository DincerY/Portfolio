namespace Portfolio.Application.DTOs;

public class CreateArticleDTO
{
    public string Title { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public List<int> Authors { get; set; }
    public List<int> Categories { get; set; }
}