﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Common.Response;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using ValidationException = Portfolio.CrossCuttingConcerns.Exceptions.ValidationException;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger _logger;
    public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        _logger.LogInformation("deneme deneme dneeme");
        var res = _categoryService.GetCategories().ToList();
        return Ok(res);
    }
 

    [HttpGet("{id}")]
    public IActionResult GetCategoryById(int id)
    {
        _logger.LogWarning("Get method was called");

        //Aşağıda ki yaklaşım biraz yanlış çünkü o an başka bir hata meydana gelmişte olabilir fakat
        //hangi hata gelirse gelsin biz NotFound dönücez belkide veri tabanında bir sıkıntı var
        //bunun çözümünü ileride ele alıcam. Çözüm döndürülen hataları özelleştirmek. Mesela id değeri
        //veri tabanında yoksa KeyNotFoundException gibi bir exception döndürmek ve o exception dönmesi
        //durumunda id nin olmadığını anlamak ve hatayı ona göre işlemek.

        //Yukarıda bahsettiğim durumun bir nevi bir çözümü ama daha farklı bir yaklaşımı ileride uygulayacağım.
        var dto = new EntityIdDTO() { Id = id };

        var category = _categoryService.GetCategoryById(dto);
        return Ok(category);
    }

    [HttpGet("getCategoriesByIds")]
    public IActionResult GetCategoriesByIds([FromQuery]List<int> ids)
    {
        var dtos = ids.Select(id => new EntityIdDTO() { Id = id }).ToList();
        var categories = _categoryService.GetCategoriesByIds(dtos);
        return Ok(categories);
        
    }

    [HttpGet("getCategoryArticles/{id}")]
    public IActionResult GetCategoryWithArticles(int id)
    {
        var dto = new EntityIdDTO() { Id = id };
        var categories = _categoryService.GetArticlesByCategoryId(dto);
        return Ok(categories);
    }

    [HttpPost]
    public ActionResult<int> AddCategory(CreateCategoryDTO dto)
    {
        var added = _categoryService.AddCategory(dto);
        return Ok(added);
    }


    [HttpGet("deneme")]
    public IEnumerable<int> Deneme()
    {
        for (int i = 0; i < 5; i++)
        {
            Thread.Sleep(1000);
            yield return i;
        }
    }
}