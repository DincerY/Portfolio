using Portfolio.Domain.Entities;

namespace Portfolio.Application.DTOs;

public class ArticleDTO
{
    public string Title { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public List<int> Authors { get; set; }
}