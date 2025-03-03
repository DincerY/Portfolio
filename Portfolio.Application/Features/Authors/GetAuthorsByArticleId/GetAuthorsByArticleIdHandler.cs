using AutoMapper;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Authors.GetAuthorsByArticleId;

public class GetAuthorsByArticleIdHandler : IRequestHandler<GetAuthorsByArticleIdRequest, IEnumerable<AuthorDTO>>
{
    //TODO : bu kısımıda düzelticez
    private readonly IAuthorRepository _authorRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetAuthorsByArticleIdHandler(IMapper mapper, IAuthorRepository authorRepository, IArticleRepository articleRepository)
    {
        _mapper = mapper;
        _authorRepository = authorRepository;
        _articleRepository = articleRepository;
    }

    public async Task<IEnumerable<AuthorDTO>> Handle(GetAuthorsByArticleIdRequest request, CancellationToken cancellationToken)
    {
        var authors = _articleRepository.GetByIdWithRelation(request.Id, aut => aut.ArticleAuthors);
        if (authors == null)
        {
            throw new NotFoundException("There is no article in the entered id");
        }
        return _mapper.Map<List<AuthorDTO>>(authors);
        return null;
    }
}