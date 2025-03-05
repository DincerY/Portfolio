using AutoMapper;
using MediatR;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Articles.GetArticlesWithRelation;

public class GetArticlesWithRelationHandler : IRequestHandler<GetArticlesWithRelationRequest,IEnumerable<GetArticlesWithRelationResponse>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetArticlesWithRelationHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetArticlesWithRelationResponse>> Handle(GetArticlesWithRelationRequest request, CancellationToken cancellationToken)
    {
        var articles = _articleRepository.GetAllWithRelation(art => art.Authors, art => art.Categories);

        return _mapper.Map<IEnumerable<GetArticlesWithRelationResponse>>(articles);
    }
}