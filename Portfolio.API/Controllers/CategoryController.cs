using FluentValidation;
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
        //Aşağıda ki yaklaşım biraz yanlış çünkü o an başka bir hata meydana gelmişte olabilir fakat
        //hangi hata gelirse gelsin biz NotFound dönücez belkide veri tabanında bir sıkıntı var
        //bunun çözümünü ileride ele alıcam. Çözüm döndürülen hataları özelleştirmek. Mesela id değeri
        //veri tabanında yoksa KeyNotFoundException gibi bir exception döndürmek ve o exception dönmesi
        //durumunda id nin olmadığını anlamak ve hatayı ona göre işlemek.

        //Yukarıda bahsettiğim durumun bir nevi bir çözümü ama daha farklı bir yaklaşımı ileride uygulayacağım.
        try
        {
            var res = _categoryService.GetCategoryById(new EntityIdDTO() { Id = id });
            return Ok(res);
        }
        catch (ValidationException validationException)
        {
            return BadRequest(new { message = validationException.Message });
        }
        catch (Exception e)
        {
            return NotFound(new {message = e.Message});
        }
    }

    [HttpGet("getCategoriesByIds")]
    public IActionResult GetCategoriesByIds([FromQuery]List<int> ids)
    {
        try
        {
            var res = _categoryService.GetCategoriesByIds(ids.Select(id => new EntityIdDTO() { Id = id }).ToList());
            return Ok(res);
        }
        catch (ValidationException validationException)
        {
            return BadRequest(new { message = validationException.Message });
        }
        catch (Exception e)
        {
            return NotFound(new { message = e.Message });
        }
        
    }

    [HttpGet("getCategoryArticles/{id}")]
    public IActionResult GetCategoryWithArticles(int id)
    {
        try
        {
            var res = _categoryService.GetArticlesByCategoryId(new EntityIdDTO(){Id = id});
            return Ok(res);
        }
        catch (Exception e)
        {
            return NotFound(new {message = e.Message});
        }
        
    }

    [HttpPost]
    public ActionResult<int> AddCategory(CreateCategoryDTO dto)
    {
        var res = _categoryService.AddCategory(dto);
        return Ok(res);
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