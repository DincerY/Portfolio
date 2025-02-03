using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;

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

    [HttpGet("{id}")]
    public IActionResult GetArticle(int id)
    {
        var article = _service.GetArticleById(id);
        return Ok(article);
    }
    [HttpPost]
    public IActionResult AddArticle(Article article)
    {
        Console.WriteLine();
        return _service.AddArticle(article) == 1 ? Ok() : BadRequest();
    }
}