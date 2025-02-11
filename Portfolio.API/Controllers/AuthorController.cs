using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _service;

    public AuthorController(IAuthorService auhtorService)
    {
        _service = auhtorService;
    }

    //Bu fonksiyon sadece yazarları getirmeli ilişkili olan kısımları başka fonksiyonlar ile halletmeliyiz
    [HttpGet]
    public IActionResult GetAuthors()
    {
        var authors = _service.GetAuthors();
        return Ok(authors);
    }
    [HttpGet("getAuthorsByIds")]
    public IActionResult GetAuthorsByIds([FromQuery]List<int> ids)
    {
        try
        {
            var authors = _service.GetAuthorsByIds(ids.Select(id => new EntityIdDTO() { Id = id }).ToList());
            return Ok(authors);
        }
        catch (ValidationException validationException)
        {
            return BadRequest(new {message = validationException.Message});
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message});
        }
        
    }

    [HttpGet("getAuthorArticles/{authorId}")]
    public IActionResult GetArticleAuthors(int authorId)
    {
        try
        {
            var articleDtos = _service.GetArticlesByAuthorId(new EntityIdDTO() { Id = authorId });
            return Ok(articleDtos);
        }
        catch (ValidationException validationException)
        {
            return BadRequest(new { message = validationException.Message});
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
        
    }

    [HttpPost]
    public IActionResult AddAuthor(CreateAuthorDTO dto)
    {
        var res = _service.AddAuthor(dto);
        return Ok(res);
    }

    
}