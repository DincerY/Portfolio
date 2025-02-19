﻿using Microsoft.AspNetCore.Mvc;
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
    [HttpGet("{id}")]
    public IActionResult GetAuthorById(int id)
    {
        var dto = new EntityIdDTO() { Id = id };
        var articleDtos = _service.GetAuthorById(dto);
        return Ok(articleDtos);
    }
    [HttpGet("getAuthorsByIds")]
    public IActionResult GetAuthorsByIds([FromQuery]List<int> ids)
    {
        var dtos = ids.Select(id => new EntityIdDTO() { Id = id }).ToList();
        var authors = _service.GetAuthorsByIds(dtos);
        return Ok(authors);
    }

    [HttpGet("getAuthorArticles/{authorId}")]
    public IActionResult GetArticleAuthors(int authorId)
    {
        var dto = new EntityIdDTO() { Id = authorId };
        var articleDtos = _service.GetArticlesByAuthorId(dto);
        return Ok(articleDtos);
    }

    [HttpPost]
    public IActionResult AddAuthor(CreateAuthorDTO dto)
    {
        var added = _service.AddAuthor(dto);
        return Ok(added);
    }

    
}