using Microsoft.Extensions.Logging;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    
    private readonly ILogger<CategoryService> _logger;
    public CategoryService(ICategoryRepository categoryRepository,ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
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

    public CategoryDTO GetCategoryById(EntityIdDTO dto)
    {
        var category = _categoryRepository.GetById(dto.Id);
        if (category == null)
        {
            throw new NotFoundException("There is no category in the entered id");
        }
        return new CategoryDTO()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
        };
    }

    public List<CategoryDTO> GetCategoriesByIds(List<EntityIdDTO> dtos)
    {
        var categories = _categoryRepository.GetByIds(dtos.Select(dto => dto.Id).ToList());
        if (categories.Count() != dtos.Count)
        {
            throw new NotFoundException("There is no category in the entered ids");
        }

        return categories.Select(cat => new CategoryDTO()
        {
            Id = cat.Id,
            Description = cat.Description,
            Name = cat.Name
        }).ToList();
    }


    public int AddCategory(CreateCategoryDTO dto)
    {
        bool isNameExist = _categoryRepository.IsExists(cat => cat.Name.ToLower() == dto.Name.ToLower());
        if (isNameExist)
        {
            throw new BusinessException("Category name already exist.");
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
            _logger.LogCritical("Category added...");
            return addedCategory.Id;
        }
        else
        {
            throw new BusinessException("Adding is not successful");
        }
    }

    public List<ArticlesWithCategoryDTO> GetArticlesByCategoryId(EntityIdDTO dto)
    {
        var category = _categoryRepository.GetByIdWithRelation(dto.Id, cat => cat.Articles);
        if (category == null)
        {
            throw new NotFoundException("There is no category in the entered id");
        }

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