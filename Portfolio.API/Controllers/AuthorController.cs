using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Articles.GetArticlesByAuthorId;
using Portfolio.Application.Features.Authors.CreateAuthor;
using Portfolio.Application.Features.Authors.GetAuthorById;
using Portfolio.Application.Features.Authors.GetAuthors;
using Portfolio.Application.Features.Authors.GetAuthorsByArticleId;
using Portfolio.Application.Features.Authors.GetAuthorsByIds;

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
    //TODO : diğer controller taraflarında da bazı yerleri taşıyacağım
    /*[HttpGet("getAuthorArticles/{authorId}")]
    public async Task<IActionResult> GetArticleAuthors(int authorId)
    {
        var articleDtos = await _mediator.Send(new GetArticlesByAuthorIdRequest() { Id = authorId });
        return Ok(articleDtos);
    }*/

    [HttpGet("getArticleAuthors/{articleId}")]
    public async Task<IActionResult> GetAuthors(int articleId)
    {
        var articleDtos = await _mediator.Send(new GetAuthorsByArticleIdRequest() { Id = articleId });
        return Ok(articleDtos);
    }

    [HttpPost]
    public async Task<IActionResult> AddAuthor([FromBody] CreateAuthorRequest request)
    {
        var added = await _mediator.Send(request);
        return Ok(added);
    }

    
}