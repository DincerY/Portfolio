using AutoMapper;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Articles.GetArticlesByAuthorId;

public class GetArticlesByAuthorIdHandler : IRequestHandler<GetArticlesByAuthorIdRequest, IEnumerable<ArticleDTO>>
{
    //TODO :
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetArticlesByAuthorIdHandler(IMapper mapper, IArticleRepository articleRepository)
    {
        _mapper = mapper;
        _articleRepository = articleRepository;
    }

    public async Task<IEnumerable<ArticleDTO>> Handle(GetArticlesByAuthorIdRequest request, CancellationToken cancellationToken)
    {
        //Diğer tabloyu eklemeden sadece ara tabloya giderek işlem yaptık.
        var articles = _articleRepository.GetByIdWithRelation(request.Id, art => art.ArticleAuthors);
        if (articles == null)
        {
            throw new NotFoundException("There is no author in the entered id");
        }

        return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
    }
}