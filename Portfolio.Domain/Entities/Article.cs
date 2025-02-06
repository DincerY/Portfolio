namespace Portfolio.Domain.Entities;

public class Article : BaseEntity
{
    public string Title { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public List<Author> Authors { get; set; }
    public List<Category> Categories { get; set; }
    public List<ArticleAuthor> ArticleAuthors { get; set; }
    public List<ArticleCategory> ArticleCategories { get; set; }

}