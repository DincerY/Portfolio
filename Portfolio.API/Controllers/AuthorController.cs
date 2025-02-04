using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _auhtorService;

    public AuthorController(IAuthorService auhtorService)
    {
        _auhtorService = auhtorService;
    }

    [HttpGet]
    public IActionResult GetAuthors()
    {
        var authors = _auhtorService.GetAuthors();
        return Ok(authors);
    }

    [HttpPost]
    public IActionResult AddAuthor(CreateAuthorDTO dto)
    {
        var res = _auhtorService.AddAuthor(dto);
        return Ok(res);
    }
}