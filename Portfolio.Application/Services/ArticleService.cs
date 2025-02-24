using AutoMapper;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;

//using ValidationException = FluentValidation.ValidationException;

namespace Portfolio.Application.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public ArticleService(IArticleRepository articlesRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _articleRepository = articlesRepository;
        _authorRepository = authorRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public IEnumerable<ArticleDTO> GetArticles()
    {
        var articles = _articleRepository.GetAll();
        return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
    }

    public IEnumerable<ArticleWithRelationsDTO> GetArticlesWithRelation()
    {
        List<ArticleWithRelationsDTO> dtos = new();
        var articles = _articleRepository.GetAllWithRelation(art => art.Authors, art => art.Categories);

        return _mapper.Map<IEnumerable<ArticleWithRelationsDTO>>(articles);
    }

    public ArticleDTO GetArticleById(EntityIdDTO dto)
    {
        
        Article article = _articleRepository.GetById(dto.Id);
        if (article == null)
        {
            throw new NotFoundException("There is no article in the entered id");
        }
        return _mapper.Map<ArticleDTO>(article);
    }

    public ArticleWithRelationsDTO GetArticleWithRelationById(EntityIdDTO dto)
    {
        Article article = _articleRepository.GetByIdWithRelation(dto.Id,art => art.Authors, art => art.Categories);

        if (article == null)
        {
            throw new NotFoundException("There is no article in the entered id");
        }
        return _mapper.Map<ArticleWithRelationsDTO>(article);
    }

    public IEnumerable<ArticleDTO> GetArticlesByIds(List<EntityIdDTO> dtos)
    {
        var articles = _articleRepository.GetWhere(art => dtos.Select(dto => dto.Id).Contains(art.Id)).ToList();
        if (articles.Count != dtos.Count)
        {
            throw new NotFoundException("There is no article in the entered ids");
        }
        return _mapper.Map<List<ArticleDTO>>(articles);
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

    public IEnumerable<AuthorDTO> GetAuthorsByArticleId(EntityIdDTO dto)
    {
        var authorList = _articleRepository.GetByIdWithRelation(dto.Id,art => art.Authors).Authors;
        if (authorList == null)
        {
            throw new NotFoundException("There is no article in the entered id");
        }
        return _mapper.Map<List<AuthorDTO>>(authorList);
    }

}