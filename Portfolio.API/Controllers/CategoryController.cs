using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var res = _categoryService.GetCategories();
        return Ok(res);
    }

    [HttpGet("{id}")]
    public IActionResult GetCategoryById(int id)
    {
        var res = _categoryService.GetCategoryById(id);
        return Ok(res);
    }
    [HttpGet("/api/getCategoryArticles/{id}")]
    public IActionResult GetCategoryWithArticles(int id)
    {
        var res = _categoryService.GetArticlesByCategoryId(id);
        return Ok(res);
    }

    [HttpPost]
    public IActionResult AddCategory(CreateCategoryDTO dto)
    {
        var res = _categoryService.AddCategory(dto);
        return Ok(res);
    }
}