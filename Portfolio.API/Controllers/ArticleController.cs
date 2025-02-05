﻿using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("{id}")]
    public IActionResult GetArticle(int id)
    {
        var articleDto = _service.GetArticleById(id);
        return Ok(articleDto);
    }

    [HttpGet("/api/getArticleAuthors/{articleId}")]
    public IActionResult GetArticleAuthors(int articleId)
    {
        var authorDTOs = _service.GetAuthorsByArticleId(articleId);
        return Ok(authorDTOs);
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