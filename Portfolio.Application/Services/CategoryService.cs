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
    private readonly IValidator<EntityIdDTO> _entityIdValidator;
    private readonly IValidator<List<EntityIdDTO>> _entityIdListValidator;
    public CategoryService(ICategoryRepository categoryRepository, IValidator<CreateCategoryDTO> createCategoryValidator, IValidator<EntityIdDTO> entityIdValidator, IValidator<List<EntityIdDTO>> entityIdListValidator)
    {
        _categoryRepository = categoryRepository;
        _createCategoryValidator = createCategoryValidator;
        _entityIdValidator = entityIdValidator;
        _entityIdListValidator = entityIdListValidator;
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
        var res = _entityIdValidator.Validate(dto);
        if (!res.IsValid)
        {
            throw new ValidationException(res.Errors);
        }

        var category = _categoryRepository.GetById(dto.Id);
        if (category == null)
        {
            throw new Exception("There is no category in the entered id");
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
        var res = _entityIdListValidator.Validate(dtos);
        if (!res.IsValid)
        {
            throw new ValidationException(res.Errors);
        }

        var categories = _categoryRepository.GetByIds(dtos.Select(dto => dto.Id).ToList());
        if (categories.Count() != dtos.Count)
        {
            throw new Exception("There is no category in the entered ids");
        }

        return categories.Select(cat => new CategoryDTO()
        {
            Id = cat.Id,
            Description = cat.Description,
            Name = cat.Name
        }).ToList();
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

    public List<ArticlesWithCategoryDTO> GetArticlesByCategoryId(EntityIdDTO dto)
    {
        var res = _entityIdValidator.Validate(dto);
        if (!res.IsValid)
        {
            throw new ValidationException(res.Errors);
        }

        var category = _categoryRepository.GetByIdWithRelation(dto.Id, cat => cat.Articles);
        if (category == null)
        {
            throw new Exception("There is no category in the entered id");
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