namespace Portfolio.Domain.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime PublishedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}