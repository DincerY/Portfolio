using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Common;
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
    public async Task<IActionResult> GetAuthors([FromQuery] PageRequest request)
    {
        var authors = await _mediator.Send(new GetAuthorsRequest()
        {
            PageSize = request.PageSize,
            PageNumber = request.PageNumber
        });
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

    [HttpGet("getAuthorsByArticleId/{id}")]
    public async Task<IActionResult> GetAuthors(int id)
    {
        var articleDtos = await _mediator.Send(new GetAuthorsByArticleIdRequest() { Id = id });
        return Ok(articleDtos);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> AddAuthor([FromBody] CreateAuthorRequest request)
    {
        var added = await _mediator.Send(request);
        return Ok(added);
    }
    [Authorize(Roles = "admin")]
    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        return Ok(new
        {
            Message = "Access granted!",
            User = User.Identity.Name,
            IsAuthenticated = User.Identity.IsAuthenticated
        });
    }

}