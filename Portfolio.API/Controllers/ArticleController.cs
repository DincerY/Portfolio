using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _service;

    public ArticleController(IArticleService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetArticles()
    {
        var articles = _service.GetArticles();
        return Ok(articles);
    }
    [HttpGet("getArticlesByIds")]
    public IActionResult GetArticlesWithIds([FromQuery]List<int> ids)
    {
        try
        {
            var articles = _service.GetArticlesByIds(ids.Select(id => new EntityIdDTO() { Id = id }).ToList());
            return Ok(articles);
        }
        catch (ValidationException validationException)
        {
            return BadRequest(new { message = validationException.Message });
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
        
    }
    [HttpGet("getArticlesWithRelation")]
    public IActionResult GetArticlesWithRelation()
    {
        var articles = _service.GetArticlesWithRelation();
        return Ok(articles);
    }

    [HttpGet("{id}")]
    public IActionResult GetArticleById(int id)
    {
        var articleDto = _service.GetArticleById(new EntityIdDTO(){Id = id});
        return Ok(articleDto);
    }

    [HttpGet("getArticleWithRelation/{id}")]
    public IActionResult GetArticleWithRelationById(int id)
    {
        try
        {
            var articleDto = _service.GetArticleWithRelationById(new EntityIdDTO() { Id = id });
            return Ok(articleDto);
        }
        catch (ValidationException validationException)
        {
            return BadRequest(new {message = validationException.Message});
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
        
    }

    [HttpGet("getArticleAuthors/{articleId}")]
    public IActionResult GetArticleAuthors(int articleId)
    {
        try
        {
            var authorDTOs = _service.GetAuthorsByArticleId(new EntityIdDTO() { Id = articleId });
            return Ok(authorDTOs);
        }
        catch (ValidationException validationException)
        {
            return BadRequest(new { message = validationException.Message });
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
        
    }


    //Veri tabanında kullandığımız nesleri direkt olarak dışarıdan alamak hem güvenlik hem işleyiş açısından
    //problem olabilir. Örnek olarak burada bir makalenin yazarlarını bütün olarak almaktansa sadace
    //idlerini bir liste olarak alıp bunları ilişkilendiricez.
    [HttpPost]
    public IActionResult AddArticle(CreateArticleDTO dto)
    {
        //İşin çalışıp çalışmadığını servis katmanında kontrol etmeliyiz.
        int res = _service.AddArticle(dto);
        return Ok(res);
    }
}