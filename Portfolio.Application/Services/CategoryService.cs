using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IEnumerable<Category> GetCategories()
    {
        return _categoryRepository.GetAll();
    }

    public Category GetCategoryById(int id)
    {
        return _categoryRepository.GetById(id);
    }

    public int AddCategory(CreateCategoryDTO dto)
    {
        Category category = new()
        {
            Name = dto.Name,
            Description = dto.Description,
            PublishedDate = DateTime.UtcNow
        };
            
        return _categoryRepository.Add(category);
    }
}