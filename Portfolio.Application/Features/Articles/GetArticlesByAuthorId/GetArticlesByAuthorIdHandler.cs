using AutoMapper;
using MediatR;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Application.Interfaces.Repositories;

namespace Portfolio.Application.Features.Articles.GetArticlesByAuthorId;

public class GetArticlesByAuthorIdHandler : IRequestHandler<GetArticlesByAuthorIdRequest, IEnumerable<GetArticlesByAuthorIdResponse>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetArticlesByAuthorIdHandler(IMapper mapper, IArticleRepository articleRepository)
    {
        _mapper = mapper;
        _articleRepository = articleRepository;
    }

    public async Task<IEnumerable<GetArticlesByAuthorIdResponse>> Handle(GetArticlesByAuthorIdRequest request, CancellationToken cancellationToken)
    {
        //Diğer tabloyu eklemeden sadece ara tabloya giderek işlem yaptık.
        var articles = _articleRepository.GetWhere(art => art.ArticleAuthors.Any(aa => aa.ArticleId == art.Id && aa.AuthorId == request.Id));
        if (articles == null)
        {
            throw new NotFoundException("There is no author in the entered id");
        }

        return _mapper.Map<IEnumerable<GetArticlesByAuthorIdResponse>>(articles);
    }
}