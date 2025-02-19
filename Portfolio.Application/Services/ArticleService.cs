using FluentValidation;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Common.Response;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;
using ValidationException = Portfolio.CrossCuttingConcerns.Exceptions.ValidationException;

//using ValidationException = FluentValidation.ValidationException;

namespace Portfolio.Application.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ArticleService(IArticleRepository articlesRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository)
    {
        _articleRepository = articlesRepository;
        _authorRepository = authorRepository;
        _categoryRepository = categoryRepository;
    }

    public IEnumerable<ArticleDTO> GetArticles()
    {
        var articles = _articleRepository.GetAll();
        return articles.Select(art => new ArticleDTO()
        {
            Id = art.Id,
            Title = art.Title,
            Content = art.Content,
            Name = art.Name
        });
    }

    public IEnumerable<ArticleWithRelationsDTO> GetArticlesWithRelation()
    {
        List<ArticleWithRelationsDTO> dtos = new();
        var articles = _articleRepository.GetAllWithRelation(art => art.Authors, art => art.Categories);
        foreach (var article in articles)
        {
            List<AuthorDTO> authorDtos = new();
            List<CategoryDTO> categoryDtos = new();
            foreach (var articleAuthor in article.Authors)
            {
                authorDtos.Add(new AuthorDTO()
                {
                    Id = articleAuthor.Id,
                    Name = articleAuthor.Name,
                    Surname = articleAuthor.Surname
                });
            }

            foreach (var articleCategory in article.Categories)
            {
                categoryDtos.Add(new CategoryDTO()
                {
                    Id = articleCategory.Id,
                    Name = articleCategory.Name,
                    Description = articleCategory.Description
                });
            }
            dtos.Add(new ArticleWithRelationsDTO()
            {
                Id = article.Id,
                Name = article.Name,
                Title = article.Title,
                Content = article.Content,
                Authors = authorDtos,
                Categories = categoryDtos
            });
        }
        return dtos;
    }

    public ArticleDTO GetArticleById(EntityIdDTO dto)
    {
        
        Article article = _articleRepository.GetById(dto.Id);
        if (article == null)
        {
            throw new NotFoundException("There is no article in the entered id");
        }
        ArticleDTO articleDto = new ArticleDTO()
        {
            Id = article.Id,
            Name = article.Name,
            Content = article.Content,
            Title = article.Title
        };
        return articleDto;
    }

    public ArticleWithRelationsDTO GetArticleWithRelationById(EntityIdDTO dto)
    {
        Article article = _articleRepository.GetByIdWithRelation(dto.Id,art => art.Authors, art => art.Categories);

        if (article == null)
        {
            throw new NotFoundException("There is no article in the entered id");
        }

        List<AuthorDTO> authorDtos = article.Authors.Select(aut => new AuthorDTO()
        {
            Id = aut.Id,
            Name = aut.Name,
            Surname = aut.Surname
        }).ToList();

        List<CategoryDTO> categoryDtos = article.Categories.Select(cat => new CategoryDTO()
        {
            Id = cat.Id,
            Name = cat.Name,
            Description = cat.Description
        }).ToList();

        ArticleWithRelationsDTO dtos = new()
        {
            Id = article.Id,
            Authors = authorDtos,
            Categories = categoryDtos,
            Content = article.Content,
            Name = article.Name,
            Title = article.Title,
        };
        return dtos;
    }

    public List<Article> GetArticlesByIds(List<EntityIdDTO> dtos)
    {
        var articles = _articleRepository.GetWhere(art => dtos.Select(dto => dto.Id).Contains(art.Id)).ToList();
        if (articles.Count != dtos.Count)
        {
            throw new NotFoundException("There is no article in the entered ids");
        }
        return articles;
    }

    //Makale oluştururken makalenin kesinlikle bir yazarı olabilir fakat bir yazar oluştururken
    //o yazarın o ana kadar bir makalesi olmayabilir. Arada ki ilişki çoka çok ilişki türü.
    public int AddArticle(CreateArticleDTO dto)
    {
        bool isArticleExits = _articleRepository.IsExists(art => art.Name.ToLower() == dto.Name.ToLower());
        if (isArticleExits)
        {
            throw new BusinessException("Article's name is already exist");
        }

        isArticleExits = _articleRepository.IsExists(art => art.Title.ToLower() == dto.Title.ToLower());
        if (isArticleExits)
        {
            throw new BusinessException("Article's title is already exist");
        }

        //Business Logic
        //Bütün veri yerine sadece id değerlerini çekip bunların kontrolünü yapıcam
        //Daha sonra bu id değerlerini ara tablolara eklicem yada yeni nesneler oluşturup
        //sadece onların id kısımlarını doldurucam. Aşağıda ki gibi

        /*var test = existAuthorIds.Select(id => new Author { Id = id }).ToList();*/

        //_authorRepository.GetWhere().Select()
        var existAuthors = _authorRepository.GetByIds(dto.Authors).ToList();
        if (existAuthors.Count != dto.Authors.Count)
        {
            throw new NotFoundException("Entered invalid author IDs.");
        }

        var existCategories = _categoryRepository.GetByIds(dto.Categories).ToList();
        if (existCategories.Count != dto.Categories.Count)
        {
            throw new NotFoundException("Entered invalid categories IDs.");
        }

        Article article = new Article()
        {
            Content = dto.Content,
            Name = dto.Name,
            PublishedDate = DateTime.UtcNow,
            Title = dto.Title,
            Authors = existAuthors,
            Categories = existCategories
        };
        var addedArticle = _articleRepository.Add(article);

        //bir önce ki yaklaşımda ara tabloyu kullanarak ekleme yapıyordum fakat kontrol için zaten verileri
        //verileri getirmek zorundayım getirmişken de ekleme işlemini direkt navigation property 
        //üzerinden yapmayı daha basit buldum.

        if (addedArticle == null)
        {
            throw new BusinessException("Adding is not successful");
        }
        else
        {
            return addedArticle.Id;
        }
    }

    public List<AuthorDTO> GetAuthorsByArticleId(EntityIdDTO dto)
    {
        List<AuthorDTO> dtos = new List<AuthorDTO>();
        var articleList = _articleRepository.GetByIdWithRelation(dto.Id,art => art.Authors);

        if (articleList == null)
        {
            throw new NotFoundException("There is no article in the entered id");
        }

        var authorList = articleList.Authors;

        foreach (var author in authorList)
        {
            dtos.Add(new AuthorDTO()
            {
                Id = author.Id,
                Name = author.Name,
                Surname = author.Surname,
            });
        }
        return dtos;
    }

}