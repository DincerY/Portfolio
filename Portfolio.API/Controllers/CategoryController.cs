using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Categories.CreateCategory;
using Portfolio.Application.Features.Categories.GetArticlesByCategoryId;
using Portfolio.Application.Features.Categories.GetCategories;
using Portfolio.Application.Features.Categories.GetCategoriesByIds;
using Portfolio.Application.Features.Categories.GetCategoryById;
using Portfolio.CrossCuttingConcerns.Exceptions;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;
    public CategoryController(ILogger<CategoryController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        _logger.LogInformation("deneme deneme dneeme");
        var res = _mediator.Send(new GetCategoriesRequest()).Result;
        return Ok(res);
    }
 

    [HttpGet("{id}")]
    public IActionResult GetCategoryById(int id)
    {
        if (id <= 0)
        {
            throw new BadRequestException("Id must be greater than zero.");
        }
        _logger.LogWarning("Get method was called");

        //Aşağıda ki yaklaşım biraz yanlış çünkü o an başka bir hata meydana gelmişte olabilir fakat
        //hangi hata gelirse gelsin biz NotFound dönücez belkide veri tabanında bir sıkıntı var
        //bunun çözümünü ileride ele alıcam. Çözüm döndürülen hataları özelleştirmek. Mesela id değeri
        //veri tabanında yoksa KeyNotFoundException gibi bir exception döndürmek ve o exception dönmesi
        //durumunda id nin olmadığını anlamak ve hatayı ona göre işlemek.

        //Yukarıda bahsettiğim durumun bir nevi bir çözümü ama daha farklı bir yaklaşımı ileride uygulayacağım.

        /*var dto = new EntityIdDTO() { Id = id };

        var category = _categoryService.GetCategoryById(dto);*/

        var category = _mediator.Send(new GetCategoryByIdRequest() { Id = id }).Result;

        return Ok(category);
    }

    [HttpGet("getCategoriesByIds")]
    public IActionResult GetCategoriesByIds([FromQuery]List<int> ids)
    {
        foreach (var id in ids)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Ids must be greater than zero.");
            }
        }

        var categories = _mediator.Send(new GetCategoriesByIdsRequest() { Ids = ids }).Result;
        return Ok(categories);
        
    }

    [HttpGet("getCategoryArticles/{id}")]
    public IActionResult GetArticlesWithCategoryId(int id)
    {
        if (id <= 0)
        {
            throw new BadRequestException("Id must be greater than zero.");
        }

        var articles = _mediator.Send(new GetArticlesByCategoryIdRequest() { Id = id }).Result;
        return Ok(articles);
    }

    [HttpPost]
    public ActionResult<int> AddCategory([FromBody]CreateCategoryRequest request)
    {
        var response = _mediator.Send(request).Result;
        if (response != null)
        {
            return Ok(response);

        }
        else
        {
            return BadRequest("Added not success");
        }
    }


    [HttpGet("deneme")]
    public IEnumerable<int> Deneme()
    {
        for (int i = 0; i < 5; i++)
        {
            Thread.Sleep(1000);
            yield return i;
        }
    }
}