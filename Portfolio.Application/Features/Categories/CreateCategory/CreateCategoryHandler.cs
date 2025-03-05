﻿using AutoMapper;
using MediatR;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Categories.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest,CreateCategoryResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CreateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        bool isNameExist = _categoryRepository.IsExists(cat => cat.Name.ToLower() == request.Name.ToLower());
        if (isNameExist)
        {
            throw new BusinessException("Category name already exist.");
        }

        Category category = _mapper.Map<Category>(request);
        //TODO : Burada belki iki farklı mapper kullanırım
        var addedCategory = _categoryRepository.Add(category);
        if (addedCategory != null)
        {
            return new CreateCategoryResponse()
            {
                Id = addedCategory.Id,
                Name = addedCategory.Name,
                CreatedDate = category.PublishedDate
            };
        }
        else
        {
            throw new BusinessException("Adding is not successful");
        }
    }
}