using FluentValidation;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<CreateCategoryDTO> _createCategoryValidator;
    public CategoryService(ICategoryRepository categoryRepository, IValidator<CreateCategoryDTO> createCategoryValidator)
    {
        _categoryRepository = categoryRepository;
        _createCategoryValidator = createCategoryValidator;
    }

    public IEnumerable<CategoryDTO> GetCategories()
    {
        var category = _categoryRepository.GetAll();
        return category.Select(cat => new CategoryDTO()
        {
            Id = cat.Id,
            Name = cat.Name,
            Description = cat.Description,
        }).ToList();
    }

    public CategoryDTO GetCategoryById(int id)
    {
        var category = _categoryRepository.GetById(id);
        return new CategoryDTO()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
        };
    }
    //Hem validasyon hemde iş mantığını bir arada yaptık.
    public int AddCategory(CreateCategoryDTO dto)
    {
        var res = _createCategoryValidator.Validate(dto);
        if (!res.IsValid)
        {
            throw new ValidationException(res.Errors);
        }

        bool isNameExist = _categoryRepository.IsExists(cat => cat.Name.ToLower() == dto.Name.ToLower());
        if (isNameExist)
        {
            throw new Exception("Category name already exist.");
        }


        Category category = new()
        {
            Name = dto.Name,
            Description = dto.Description,
            PublishedDate = DateTime.UtcNow
        };
            
        var addedCategory = _categoryRepository.Add(category);
        if (addedCategory != null)
        {
            return addedCategory.Id;
        }
        else
        {
            throw new Exception("Adding is not successful");
        }
    }

    public List<ArticlesWithCategoryDTO> GetArticlesByCategoryId(int categoryId)
    {
        var category = _categoryRepository.GetByIdWithRelation(categoryId, cat => cat.Articles);

        return category.Articles.Select(art => new ArticlesWithCategoryDTO()
        {
            Title = art.Title,
            Content = art.Content,
            Name = art.Name,
            CategoryName = category.Name,
            CategoryDescription = category.Description
        }).ToList();

    }
}