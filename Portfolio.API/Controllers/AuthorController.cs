using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Authors.CreateAuthor;
using Portfolio.Application.Features.Authors.GetArticlesByAuthorId;
using Portfolio.Application.Features.Authors.GetAuthorById;
using Portfolio.Application.Features.Authors.GetAuthors;
using Portfolio.Application.Features.Authors.GetAuthorsByIds;
using Portfolio.CrossCuttingConcerns.Exceptions;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //Bu fonksiyon sadece yazarları getirmeli ilişkili olan kısımları başka fonksiyonlar ile halletmeliyiz
    [HttpGet]
    public async Task<IActionResult> GetAuthors()
    {
        var authors = await _mediator.Send(new GetAuthorsRequest());
        return Ok(authors);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(int id)
    {
        var articleDto = await _mediator.Send(new GetAuthorByIdRequest() { Id = id });
        return Ok(articleDto);
    }
    [HttpGet("getAuthorsByIds")]
    public async Task<IActionResult> GetAuthorsByIds([FromQuery]List<int> ids)
    {
        var authors = await _mediator.Send(new GetAuthorsByIdsRequest() { Ids = ids });
        return Ok(authors);
    }

    [HttpGet("getAuthorArticles/{authorId}")]
    public async Task<IActionResult> GetArticleAuthors(int authorId)
    {
        var articleDtos = await _mediator.Send(new GetArticlesByAuthorIdRequest() { Id = authorId });
        return Ok(articleDtos);
    }

    [HttpPost]
    public async Task<IActionResult> AddAuthor([FromBody] CreateAuthorRequest request)
    {
        var added = await _mediator.Send(request);
        return Ok(added);
    }

    
}