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
    public IActionResult GetAuthors()
    {
        var authors = _mediator.Send(new GetAuthorsRequest()).Result;
        return Ok(authors);
    }
    [HttpGet("{id}")]
    public IActionResult GetAuthorById(int id)
    {
        if (id <= 0)
        {
            throw new BadRequestException("Id must be greater than zero.");
        }

        var articleDto = _mediator.Send(new GetAuthorByIdRequest() { Id = id }).Result;
        return Ok(articleDto);
    }
    [HttpGet("getAuthorsByIds")]
    public IActionResult GetAuthorsByIds([FromQuery]List<int> ids)
    {
        foreach (var id in ids)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Ids must be greater than zero.");
            }
        }

        var authors = _mediator.Send(new GetAuthorsByIdsRequest() { Ids = ids });
        return Ok(authors);
    }

    [HttpGet("getAuthorArticles/{authorId}")]
    public IActionResult GetArticleAuthors(int authorId)
    {
        if (authorId <= 0)
        {
            throw new BadRequestException("Id must be greater than zero.");
        }
        var articleDtos = _mediator.Send(new GetArticlesByAuthorIdRequest() { Id = authorId }).Result;
        return Ok(articleDtos);
    }

    [HttpPost]
    public IActionResult AddAuthor([FromBody] CreateAuthorRequest request)
    {
        var added = _mediator.Send(request).Result;
        return Ok(added);
    }

    
}