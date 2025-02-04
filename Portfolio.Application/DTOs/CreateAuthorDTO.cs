namespace Portfolio.Application.DTOs;

public class CreateAuthorDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<int> ArticleIds { get; set; }
}