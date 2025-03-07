using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Common;
using Portfolio.Application.Features.Articles.CreateArticle;
using Portfolio.Application.Features.Articles.GetArticleById;
using Portfolio.Application.Features.Articles.GetArticles;
using Portfolio.Application.Features.Articles.GetArticlesByAuthorId;
using Portfolio.Application.Features.Articles.GetArticlesByCategoryId;
using Portfolio.Application.Features.Articles.GetArticlesByIds;
using Portfolio.Application.Features.Articles.GetArticlesWithRelation;
using Portfolio.Application.Features.Articles.GetArticleWithRelationById;


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
    public async Task<IActionResult> GetArticles([FromQuery] PageRequest request)
    {
        var articles = await _mediator.Send(new GetArticlesRequest()
        {
            PageSize = request.PageSize,
            PageNumber = request.PageNumber
        });
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

    [HttpGet("getArticlesByAuthorId/{id}")]
    public async Task<IActionResult> GetArticlesAuthors(int id)
    {
        var articleDto = await _mediator.Send(new GetArticlesByAuthorIdRequest() { Id = id });
        return Ok(articleDto);
    }

    [HttpGet("getArticlesByCategoryId/{id}")]
    public async Task<IActionResult> GetArticlesByCategoryId(int id)
    {
        var articles = await _mediator.Send(new GetArticlesByCategoryIdRequest() { Id = id });
        return Ok(articles);
    }

    [HttpPost]
    public async Task<IActionResult> AddArticle([FromBody]CreateArticleRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}