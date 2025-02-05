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

    [HttpGet]
    public IActionResult GetAuthors()
    {
        var authors = _service.GetAuthors();
        return Ok(authors);
    }

    [HttpGet("/api/getAuthorArticles/{authorId}")]
    public IActionResult GetArticleAuthors(int authorId)
    {
        var articleDtos = _service.GetArticlesByAuthorId(authorId);
        return Ok(articleDtos);
    }

    [HttpPost]
    public IActionResult AddAuthor(CreateAuthorDTO dto)
    {
        var res = _service.AddAuthor(dto);
        return Ok(res);
    }

    
}