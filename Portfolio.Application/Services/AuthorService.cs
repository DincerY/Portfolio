using AutoMapper;
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
    private readonly IMapper _mapper;

    public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public IEnumerable<AuthorDTO> GetAuthors()
    {
        var authors = _authorRepository.GetAllWithRelation(aut => aut.Articles);
        return _mapper.Map<IEnumerable<AuthorDTO>>(authors);
    }

    public AuthorDTO GetAuthorById(EntityIdDTO dto)
    {
        var author = _authorRepository.GetById(dto.Id);
        if (author == null)
        {
            throw new NotFoundException("There is no author in the entered id");
        }

        return _mapper.Map<AuthorDTO>(author);
    }

    public IEnumerable<AuthorDTO> GetAuthorsByIds(List<EntityIdDTO> dtos)
    {
        var authors = _authorRepository.GetWhere(aut => dtos.Select(dto => dto.Id).Contains(aut.Id)).ToList();

        if (authors.Count != dtos.Count)
        {
            throw new NotFoundException("There is no author in the entered ids");
        }

        return _mapper.Map<IEnumerable<AuthorDTO>>(authors);
    }

    public int AddAuthor(CreateAuthorDTO dto)
    {
        bool isAuthorExist = _authorRepository.IsExists(aut => aut.Name.ToLower() == dto.Name.ToLower());
        if (isAuthorExist)
        {
            throw new BusinessException("Author's name is already exist");
        }

        Author author = _mapper.Map<Author>(dto);
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

    public IEnumerable<ArticleDTO> GetArticlesByAuthorId(EntityIdDTO dto)
    {
        var articles = _authorRepository.GetByIdWithRelation(dto.Id, aut => aut.Articles).Articles;
        if (articles == null)
        {
            throw new NotFoundException("There is no author in the entered id");
        }

        return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
    }
}