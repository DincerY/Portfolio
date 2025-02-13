using System.Runtime.InteropServices;
using FluentValidation;
using Portfolio.Application.DTOs;
using Portfolio.Application.Interfaces;
using Portfolio.Common.Response;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;
using ValidationException = Portfolio.CrossCuttingConcerns.Exceptions.ValidationException;

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
    private readonly IValidator<CreateAuthorDTO> _createAuthorValidator;
    private readonly IValidator<EntityIdDTO> _entityIdValidator;
    private readonly IValidator<List<EntityIdDTO>> _entityIdListValidator;

    public AuthorService(IAuthorRepository authorRepository, IValidator<CreateAuthorDTO> createAuthorValidator, IValidator<EntityIdDTO> entityIdValidator, IValidator<List<EntityIdDTO>> entityIdListValidator)
    {
        _authorRepository = authorRepository;
        _createAuthorValidator = createAuthorValidator;
        _entityIdValidator = entityIdValidator;
        _entityIdListValidator = entityIdListValidator;
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
        var res = _entityIdValidator.Validate(dto);
        if (!res.IsValid)
        {
            throw new ValidationException(res.Errors.Select(er => new ValidationError()
            {
                Domain = er.PropertyName,
                Message = er.ErrorMessage,
                Reason = nameof(GetAuthorById)
            }).ToList());
        }
        return _authorRepository.GetById(dto.Id);
        //!!!Id değerinin elle girilmesini engellemek aslında bir iş mantığıdır.
    }

    public List<AuthorDTO> GetAuthorsByIds(List<EntityIdDTO> dtos)
    {
        var res = _entityIdListValidator.Validate(dtos);
        if (!res.IsValid)
        {
            throw new ValidationException(res.Errors.Select(er => new ValidationError()
            {
                Domain = er.PropertyName,
                Message = er.ErrorMessage,
                Reason = nameof(GetAuthorsByIds)
            }).ToList());
        }

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
        var res = _createAuthorValidator.Validate(dto);
        if (!res.IsValid)
        {
            throw new ValidationException(res.Errors.Select(er => new ValidationError()
            {
                Domain = er.PropertyName,
                Message = er.ErrorMessage,
                Reason = nameof(AddAuthor)
            }).ToList());
        }


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
        var res = _entityIdValidator.Validate(dto);
        if (!res.IsValid)
        {
            throw new ValidationException(res.Errors.Select(er => new ValidationError()
            {
                Domain = er.PropertyName,
                Message = er.ErrorMessage,
                Reason = nameof(GetArticlesByAuthorId)
            }).ToList());
        }

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