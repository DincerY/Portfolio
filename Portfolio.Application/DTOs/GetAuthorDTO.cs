namespace Portfolio.Application.DTOs;

public class GetAuthorDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<ArticleDTO> Articles { get; set; }
}