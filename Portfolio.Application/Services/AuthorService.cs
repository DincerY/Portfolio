using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Services;
/// <summary>
///  1. Boş ürün kontrolü
///  2. Aynı isimde ürün var mı?
///  3. Negatif fiyat kontrolü
///  4. Ürünü ekleme işlemi
///  gibi gibi işlemler service tarafında yapılır
/// </summary>
public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IArticleRepository _articleRepository;

    public AuthorService(IAuthorRepository authorRepository, IArticleRepository articleRepository)
    {
        _authorRepository = authorRepository;
        _articleRepository = articleRepository;
    }

    public IEnumerable<Author> GetAuthors()
    {
        return _authorRepository.GetAll();
    }

    public Author GetAuthorById(int id)
    {
        //bu yapı aslında bir validasyon çünkü id değeri kullanıcı tarafından giriliyor
        //validasyonları daha sonra iş mantığından ayıralım.
        //TODO
        if (id < 0)
        {
            throw new Exception("Id can not less than or equal to 0");
        }
        return _authorRepository.GetById(id);
        //!!!Id değerinin elle girilmesini engellemek aslında bir iş mantığıdır.
    }

    public int AddAuthor(CreateAuthorDTO dto)
    {
        var art = _articleRepository.GetWhere(a => dto.ArticleIds.Contains(a.Id)).ToList();
        Author author = new Author()
        {
            Name = dto.Name,
            Surname = dto.Surname,
            PublishedDate = DateTime.UtcNow,
            Articles = art,
        };
        int res = _authorRepository.Add(author);
        if (res > 0)
        {
            return res;
        }
        else
        {
            throw new Exception("Adding is not successful");
        }
    }
}