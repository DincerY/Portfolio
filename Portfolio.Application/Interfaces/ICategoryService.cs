using Portfolio.Application.DTOs;
using Portfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Interfaces;

public interface ICategoryService
{
    public IEnumerable<Category> GetCategories();
    public Category GetCategoryById(int id);
    public int AddCategory(CreateCategoryDTO dto);

    /*public List<Article> GetArticlesByIds(List<int> ids);

    public int AddArticle(CreateArticleDTO dto);
    public List<AuthorDTO> GetAuthorsByArticleId(int id);*/
}