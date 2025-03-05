using AutoMapper;
using MediatR;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Articles.GetArticlesByIds;

public class GetArticlesByIdsHandler : IRequestHandler<GetArticlesByIdsRequest,IEnumerable<GetArticlesByIdsResponse>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetArticlesByIdsHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetArticlesByIdsResponse>> Handle(GetArticlesByIdsRequest request, CancellationToken cancellationToken)
    {
        var articles = _articleRepository.GetWhere(art => request.Ids.Contains(art.Id)).ToList();
        if (articles.Count != request.Ids.Count)
        {
            throw new NotFoundException("There is no article in the entered ids");
        }
        return _mapper.Map<List<GetArticlesByIdsResponse>>(articles);
    }
}