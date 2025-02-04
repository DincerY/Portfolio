namespace Portfolio.Domain.Entities;

public class Author : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<Article> Articles { get; set; }
}