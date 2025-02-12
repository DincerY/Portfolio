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
        var authors = _service.GetAuthorsByIds(ids.Select(id => new EntityIdDTO() { Id = id }).ToList());
        return Ok(authors);
    }

    [HttpGet("getAuthorArticles/{authorId}")]
    public IActionResult GetArticleAuthors(int authorId)
    {
        var articleDtos = _service.GetArticlesByAuthorId(new EntityIdDTO() { Id = authorId });
        return Ok(articleDtos);
    }

    [HttpPost]
    public IActionResult AddAuthor(CreateAuthorDTO dto)
    {
        var res = _service.AddAuthor(dto);
        return Ok(res);
    }

    
}