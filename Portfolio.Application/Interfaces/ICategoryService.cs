using Portfolio.Application.DTOs;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces;

public interface ICategoryService
{
    public IEnumerable<CategoryDTO> GetCategories();
    public CategoryDTO GetCategoryById(int id);
    public int AddCategory(CreateCategoryDTO dto);

    public List<ArticlesWithCategoryDTO> GetArticlesByCategoryId(int categoryId);


    /*public List<Article> GetArticlesByIds(List<int> ids);

    public int AddArticle(CreateArticleDTO dto);
    public List<AuthorDTO> GetAuthorsByArticleId(int id);*/
}