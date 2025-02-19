using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.CrossCuttingConcerns.Exceptions;
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


    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public IEnumerable<AuthorDTO> GetAuthors()
    {
        var authors = _authorRepository.GetAllWithRelation(aut => aut.Articles);
        return authors.Select(aut => new AuthorDTO()
        {
            Id = aut.Id,
            Name = aut.Name,
            Surname = aut.Surname,
        }).ToList();
    }

    public Author GetAuthorById(EntityIdDTO dto)
    {
        return _authorRepository.GetById(dto.Id);
        //!!!Id değerinin elle girilmesini engellemek aslında bir iş mantığıdır.
    }

    public List<AuthorDTO> GetAuthorsByIds(List<EntityIdDTO> dtos)
    {
        var authors = _authorRepository.GetWhere(aut => dtos.Select(dto => dto.Id).Contains(aut.Id)).ToList();

        if (authors.Count != dtos.Count)
        {
            throw new NotFoundException("There is no author in the entered ids");
        }

        return authors.Select(aut => new AuthorDTO()
        {
            Id = aut.Id,
            Name = aut.Name,
            Surname = aut.Surname,
        }).ToList();
    }

    public int AddAuthor(CreateAuthorDTO dto)
    {
        bool isAuthorExist = _authorRepository.IsExists(aut => aut.Name.ToLower() == dto.Name.ToLower());
        if (isAuthorExist)
        {
            throw new BusinessException("Author's name is already exist");
        }

        Author author = new Author()
        {
            Name = dto.Name,
            Surname = dto.Surname,
            PublishedDate = DateTime.UtcNow,
        };
        var addedAuthor = _authorRepository.Add(author);
        if (addedAuthor != null)
        {
            return addedAuthor.Id;
        }
        else
        {
            throw new BusinessException("Adding is not successful");
        }
        
    }

    public List<ArticleDTO> GetArticlesByAuthorId(EntityIdDTO dto)
    {
        List<ArticleDTO> dtos = new();
        var author = _authorRepository.GetByIdWithRelation(dto.Id, aut => aut.Articles);
        if (author == null)
        {
            throw new NotFoundException("There is no author in the entered id");
        }

        return author.Articles.Select(art => new ArticleDTO()
        {
            Id = art.Id,
            Title = art.Title,
            Name = art.Name,
            Content = art.Content
        }).ToList();
    }
}