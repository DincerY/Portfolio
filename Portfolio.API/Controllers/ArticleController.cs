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
//Burada çok büyük bir hata yaptım çünkü controllerda try catch yazılmaz.
//Bir çok sebebi var fakat en mantıklısı yeni bir hata türü oluşturduğumuzda bütün controller ları
//tek tek güncellemeliyiz. Burada çözüm middleware kullanmak.
public class ArticleController : ControllerBase
{
    private readonly IMediator _mediator;

    public ArticleController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [ResponseCache(Duration = 10)]
    [HttpGet]
    public IActionResult GetArticles()
    {
        var articles = _mediator.Send(new GetArticlesRequest()).Result;
        return Ok(articles);
        
    }
    [HttpGet("getArticlesByIds")]
    public IActionResult GetArticlesByIds([FromQuery]List<int> ids)
    {
        foreach (var id in ids)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Ids must be greater than zero.");
            }
        }
        var articles = _mediator.Send(new GetArticlesByIdsRequest() { Ids = ids }).Result;
        return Ok(articles);
    }
    [HttpGet("getArticlesWithRelation")]
    public IActionResult GetArticlesWithRelation()
    {
        var articles = _mediator.Send(new GetArticlesWithRelationRequest()).Result;
        return Ok(articles);
    }

    [HttpGet("{id}")]
    public IActionResult GetArticleById(int id)
    {
        if (id <= 0)
        {
            throw new BadRequestException("Id must be greater than zero.");
        }
        var articleDto = _mediator.Send(new GetArticleByIdRequest() { Id = id }).Result;
        return Ok(articleDto);
    }

    [HttpGet("getArticleWithRelation/{id}")]
    public IActionResult GetArticleWithRelationById(int id)
    {
        if (id <= 0)
        {
            throw new BadRequestException("Id must be greater than zero.");
        }
        var articleDto = _mediator.Send(new GetArticleWithRelationByIdRequest() { Id = id });
        return Ok(articleDto);
    }

    [HttpGet("getArticleAuthors/{articleId}")]
    public IActionResult GetArticleAuthors(int articleId)
    {
        if (articleId <= 0)
        {
            throw new BadRequestException("Id must be greater than zero.");
        }
        var articleDto = _mediator.Send(new GetArticlesByAuthorIdRequest() { Id = articleId }).Result;
        return Ok(articleDto);
        
    }

    [HttpPost]
    public IActionResult AddArticle([FromBody]CreateArticleRequest request)
    {
        var response = _mediator.Send(request).Result;
        return Ok(response);
    }
}