using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;

namespace Portfolio.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//Burada çok büyük bir hata yaptım çünkü controllerda try catch yazılmaz.
//Bir çok sebebi var fakat en mantıklısı yeni bir hata türü oluşturduğumuzda bütün controller ları
//tek tek güncellemeliyiz. Burada çözüm middleware kullanmak.
public class ArticleController : ControllerBase
{
    private readonly IArticleService _service;

    public ArticleController(IArticleService service)
    {
        _service = service;

    }
    //Burada exception handling yapmadık fakat burada da bir hata meydana gelebilir. Örnek olarak veri tabanı
    //bağlantısı kopması bazı güncelemeler ile bağımlılıkların değişmesi vs. Bunun için middleware ile bunu
    //yönetmek çok daha mantıklı.
    [HttpGet]
    public IActionResult GetArticles()
    {
        var articles = _service.GetArticles();
        return Ok(articles);
        
    }
    [HttpGet("getArticlesByIds")]
    public IActionResult GetArticlesWithIds([FromQuery]List<int> ids)
    {
        var dtos = ids.Select(id => new EntityIdDTO() { Id = id }).ToList();
        var articles = _service.GetArticlesByIds(dtos);
        return Ok(articles);
    }
    [HttpGet("getArticlesWithRelation")]
    public IActionResult GetArticlesWithRelation()
    {
        var articles = _service.GetArticlesWithRelation();
        return Ok(articles);
    }

    [HttpGet("{id}")]
    public IActionResult GetArticleById(int id)
    {
        var dto = new EntityIdDTO() { Id = id };
        var articleDto = _service.GetArticleById(dto);
        return Ok(articleDto);
    }

    [HttpGet("getArticleWithRelation/{id}")]
    public IActionResult GetArticleWithRelationById(int id)
    {
        var dto = new EntityIdDTO() { Id = id };
        var articleDto = _service.GetArticleWithRelationById(dto);
        return Ok(articleDto);
    }

    [HttpGet("getArticleAuthors/{articleId}")]
    public IActionResult GetArticleAuthors(int articleId)
    {
        var dto = new EntityIdDTO() { Id = articleId };
        var authorDTOs = _service.GetAuthorsByArticleId(dto);
        return Ok(authorDTOs);
        
    }


    //Veri tabanında kullandığımız nesleri direkt olarak dışarıdan alamak hem güvenlik hem işleyiş açısından
    //problem olabilir. Örnek olarak burada bir makalenin yazarlarını bütün olarak almaktansa sadace
    //idlerini bir liste olarak alıp bunları ilişkilendiricez.
    [HttpPost]
    public IActionResult AddArticle([FromBody]CreateArticleDTO dto)
    {
        //İşin çalışıp çalışmadığını servis katmanında kontrol etmeliyiz.
        int added = _service.AddArticle(dto);
        return Ok(added);
    }
}