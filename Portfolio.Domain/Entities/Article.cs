namespace Portfolio.Domain.Entities;

public class Article : BaseEntity
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }

}