using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Articles.CreateArticle;
using Portfolio.Application.Features.Articles.GetArticleById;
using Portfolio.Application.Features.Articles.GetArticles;
using Portfolio.Application.Features.Articles.GetArticlesByIds;
using Portfolio.Application.Features.Articles.GetArticlesWithRelation;
using Portfolio.Application.Features.Articles.GetArticleWithRelationById;
using Portfolio.Application.Features.Authors.GetArticlesByAuthorId;
using Portfolio.CrossCuttingConcerns.Exceptions;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly IMediator _mediator;

    public ArticleController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [ResponseCache(Duration = 10)]
    [HttpGet]
    public async Task<IActionResult> GetArticles()
    {
        var articles = await _mediator.Send(new GetArticlesRequest());
        return Ok(articles);
        
    }
    [HttpGet("getArticlesByIds")]
    public async Task<IActionResult> GetArticlesByIds([FromQuery]List<int> ids)
    {
        var articles = await _mediator.Send(new GetArticlesByIdsRequest() { Ids = ids });
        return Ok(articles);
    }
    [HttpGet("getArticlesWithRelation")]
    public async Task<IActionResult> GetArticlesWithRelation()
    {
        var articles = await _mediator.Send(new GetArticlesWithRelationRequest());
        return Ok(articles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetArticleById(int id)
    {
        var articleDto = await _mediator.Send(new GetArticleByIdRequest() { Id = id });
        return Ok(articleDto);
    }

    [HttpGet("getArticleWithRelation/{id}")]
    public async Task<IActionResult> GetArticleWithRelationById(int id)
    {
        var articleDto = await _mediator.Send(new GetArticleWithRelationByIdRequest() { Id = id });
        return Ok(articleDto);
    }

    [HttpGet("getArticleAuthors/{articleId}")]
    public async Task<IActionResult> GetArticleAuthors(int articleId)
    {
        var articleDto = await _mediator.Send(new GetArticlesByAuthorIdRequest() { Id = articleId });
        return Ok(articleDto);
        
    }

    [HttpPost]
    public async Task<IActionResult> AddArticle([FromBody]CreateArticleRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}