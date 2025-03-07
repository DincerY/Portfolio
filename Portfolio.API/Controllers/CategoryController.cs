using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Common;
using Portfolio.Application.Features.Categories.CreateCategory;
using Portfolio.Application.Features.Categories.GetCategories;
using Portfolio.Application.Features.Categories.GetCategoriesByArticleId;
using Portfolio.Application.Features.Categories.GetCategoriesByIds;
using Portfolio.Application.Features.Categories.GetCategoryById;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories([FromQuery] PageRequest request)
    {
        var res = await _mediator.Send(new GetCategoriesRequest()
        {
            PageSize = request.PageSize,
            PageNumber = request.PageNumber
        });
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category =await _mediator.Send(new GetCategoryByIdRequest() { Id = id });
        return Ok(category);
    }

    [HttpGet("getCategoriesByIds")]
    public async Task<IActionResult> GetCategoriesByIds([FromQuery]List<int> ids)
    {
        var categories = await _mediator.Send(new GetCategoriesByIdsRequest() { Ids = ids });
        return Ok(categories);
        
    }
    [HttpGet("getCategoriesByArticleId/{id}")]
    public async Task<IActionResult> GetCategoriesByArticleId(int id)
    {
        var categories = await _mediator.Send(new GetCategoriesByArticleIdRequest(){Id = id});
        return Ok(categories);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody]CreateCategoryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
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