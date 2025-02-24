namespace Portfolio.Domain.Entities;

public class Author : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<Article> Articles { get; set; }
    public List<ArticleAuthor> ArticleAuthors { get; set; }
}