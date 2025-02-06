namespace Portfolio.Domain.Entities;

public class ArticleAuthor
{
    public int ArticleId { get; set; }
    public int AuthorId { get; set; }
    public Article Article { get; set; } = null;
    public Author Author { get; set; } = null;
}