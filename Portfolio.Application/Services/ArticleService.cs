using FluentValidation;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<CreateArticleDTO> _createArticleValidator;


    public ArticleService(IArticleRepository articlesRepository, IValidator<CreateArticleDTO> createArticleValidator, IAuthorRepository authorRepository, ICategoryRepository categoryRepository)
    {
        _articleRepository = articlesRepository;
        _createArticleValidator = createArticleValidator;
        _authorRepository = authorRepository;
        _categoryRepository = categoryRepository;
    }

    public IEnumerable<ArticleWithRelationsDTO> GetArticles()
    {
        List<ArticleWithRelationsDTO> dtos = new();
        var articles = _articleRepository.GetAllWithRelation(art => art.Authors,art=> art.Categories);
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

    public ArticleWithRelationsDTO GetArticleById(int id)
    {
        //business logic devam edecek
        if (id <= 0)
        {
            throw new Exception("Id can not be less then or equal to 0");
        }
        Article article = _articleRepository.GetByIdWithRelation(id,art => art.Authors, art => art.Categories);
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

    public List<Article> GetArticlesByIds(List<int> ids)
    {
        /*List<Article> articles = new();
        foreach (int id in ids)
        {
            articles.Add(_articleRepository.GetById(id));
        }
        return articles;*/

        return _articleRepository.GetWhere(art => ids.Contains(art.Id)).ToList();
    }

    //Makale oluştururken makalenin kesinlikle bir yazarı olabilir fakat bir yazar oluştururken
    //o yazarın o ana kadar bir makalesi olmayabilir. Arada ki ilişki çoka çok ilişki türü.
    public int AddArticle(CreateArticleDTO dto)
    {
        var res= _createArticleValidator.Validate(dto);
        if (!res.IsValid)
        {
            throw new ValidationException(res.Errors);
        }

        bool isArticleExits = _articleRepository.IsExists(art => art.Name.ToLower() == dto.Name.ToLower());
        if (isArticleExits)
        {
            throw new Exception("Article's name is already exist");
        }

        isArticleExits = _articleRepository.IsExists(art => art.Title.ToLower() == dto.Title.ToLower());
        if (isArticleExits)
        {
            throw new Exception("Article's title is already exist");
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
            throw new Exception("Entered invalid author IDs.");
        }

        var existCategories = _categoryRepository.GetByIds(dto.Categories).ToList();
        if (existCategories.Count != dto.Categories.Count)
        {
            throw new Exception("Entered invalid categories IDs.");
        }

        Article article = new Article()
        {
            Content = dto.Content,
            Name = dto.Name,
            PublishedDate = DateTime.UtcNow,
            Title = dto.Title,
            Authors = existAuthors,
            Categories = existCategories,
        };
        var addedArticle = _articleRepository.Add(article);

        //bir önce ki yaklaşımda ara tabloyu kullanarak ekleme yapıyordum fakat kontrol için zaten verileri
        //verileri getirmek zorundayım getirmişken de ekleme işlemini direkt navigation property 
        //üzerinden yapmayı daha basit buldum.

        if (addedArticle == null)
        {
            throw new Exception("Adding is not successful");
        }
        else
        {
            return addedArticle.Id;
        }
    }

    public List<AuthorDTO> GetAuthorsByArticleId(int articleId)
    {
        List<AuthorDTO> dtos = new List<AuthorDTO>();
        var authorList = _articleRepository.GetByIdWithRelation(articleId,art => art.Authors).Authors;
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