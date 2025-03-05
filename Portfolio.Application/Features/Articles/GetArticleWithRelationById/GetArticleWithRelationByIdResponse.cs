

using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Articles.GetArticleWithRelationById;

public class GetArticleWithRelationByIdResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public List<AuthorDTO> Authors { get; set; }
    public List<CategoryDTO> Categories { get; set; }
}