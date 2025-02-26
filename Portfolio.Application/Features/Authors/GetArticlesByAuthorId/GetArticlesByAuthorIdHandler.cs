using AutoMapper;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Authors.GetArticlesByAuthorId;

public class GetArticlesByAuthorIdHandler : IRequestHandler<GetArticlesByAuthorIdRequest,IEnumerable<ArticleDTO>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public GetArticlesByAuthorIdHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ArticleDTO>> Handle(GetArticlesByAuthorIdRequest request, CancellationToken cancellationToken)
    {
        var articles = _authorRepository.GetByIdWithRelation(request.Id, aut => aut.Articles).Articles;
        if (articles == null)
        {
            throw new NotFoundException("There is no author in the entered id");
        }

        return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
    }
}