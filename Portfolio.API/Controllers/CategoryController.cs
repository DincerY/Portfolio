using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

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

    [HttpPost]
    public IActionResult AddCategory(CreateCategoryDTO dto)
    {
        var res = _categoryService.AddCategory(dto);
        return Ok(res);
    }
}