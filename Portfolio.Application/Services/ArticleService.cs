using Microsoft.EntityFrameworkCore;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAuthorRepository _authorRepository;

    public ArticleService(IArticleRepository articlesRepository, ICategoryRepository categoryRepository, IAuthorRepository authorRepository)
    {
        _articleRepository = articlesRepository;
        _categoryRepository = categoryRepository;
        _authorRepository = authorRepository;
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
        if (dto.Authors.Count <= 0)
        {
            throw new Exception("AuthorIds can not less than of equal to 0");
        }

        Article article = new Article()
        {
            Content = dto.Content,
            Name = dto.Name,
            PublishedDate = DateTime.UtcNow,
            Title = dto.Title,
        };
        article = _articleRepository.Add(article);

        //bir önce ki yaklaşımda daha fazla sorgu vardı çünkü id si girilen yazarları getiriyordum ayrıca
        //id si girilen kategorileri getiriyordum daha sonra bunları makaleye ekleyip makaleyi veri
        //tabanına öyle ekliyordum. Artık ara tabloma direkt ulaşıp idleri oraya ekliyorum.

        if (article == null)
        {
            throw new Exception("Adding is not successful");
        }

        article.ArticleAuthors = dto.Authors.Select(id => new ArticleAuthor()
        {
            ArticleId = article.Id,
            AuthorId = id
        }).ToList();

        article.ArticleCategories = dto.Categories.Select(id => new ArticleCategory()
        {
            ArticleId = article.Id,
            CategoryId = id
        }).ToList();

        int res = _articleRepository.SaveChanges();

        if (res > 0)
        {
            return article.Id;
        }
        else
        {
            throw new Exception("Adding is not successful");
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