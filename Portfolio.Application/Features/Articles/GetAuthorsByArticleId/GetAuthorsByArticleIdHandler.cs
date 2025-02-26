using AutoMapper;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Articles.GetAuthorsByArticleId;

public class GetAuthorsByArticleIdHandler : IRequestHandler<GetAuthorsByArticleIdRequest,IEnumerable<AuthorDTO>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetAuthorsByArticleIdHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorDTO>> Handle(GetAuthorsByArticleIdRequest request, CancellationToken cancellationToken)
    {
        var authorList = _articleRepository.GetByIdWithRelation(request.Id, art => art.Authors).Authors;
        if (authorList == null)
        {
            throw new NotFoundException("There is no article in the entered id");
        }
        return _mapper.Map<List<AuthorDTO>>(authorList);
    }
}