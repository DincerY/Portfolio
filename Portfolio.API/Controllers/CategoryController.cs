using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Features.Articles.GetArticlesByCategoryId;
using Portfolio.Application.Features.Categories.CreateCategory;
using Portfolio.Application.Features.Categories.GetCategories;
using Portfolio.Application.Features.Categories.GetCategoriesByArticleId;
using Portfolio.Application.Features.Categories.GetCategoriesByIds;
using Portfolio.Application.Features.Categories.GetCategoryById;
using Portfolio.CrossCuttingConcerns.Exceptions;

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
    public async Task<IActionResult> GetCategories()
    {
        var res = await _mediator.Send(new GetCategoriesRequest());
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        //Aşağıda ki yaklaşım biraz yanlış çünkü o an başka bir hata meydana gelmişte olabilir fakat
        //hangi hata gelirse gelsin biz NotFound dönücez belkide veri tabanında bir sıkıntı var
        //bunun çözümünü ileride ele alıcam. Çözüm döndürülen hataları özelleştirmek. Mesela id değeri
        //veri tabanında yoksa KeyNotFoundException gibi bir exception döndürmek ve o exception dönmesi
        //durumunda id nin olmadığını anlamak ve hatayı ona göre işlemek.

        //Yukarıda bahsettiğim durumun bir nevi bir çözümü ama daha farklı bir yaklaşımı ileride uygulayacağım.

        /*var dto = new EntityIdDTO() { Id = id };

        var category = _categoryService.GetCategoryById(dto);*/

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