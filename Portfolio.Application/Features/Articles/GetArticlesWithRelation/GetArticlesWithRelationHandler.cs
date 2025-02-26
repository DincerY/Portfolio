using AutoMapper;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Articles.GetArticlesWithRelation;

public class GetArticlesWithRelationHandler : IRequestHandler<GetArticlesWithRelationRequest,IEnumerable<ArticleWithRelationsDTO>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetArticlesWithRelationHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ArticleWithRelationsDTO>> Handle(GetArticlesWithRelationRequest request, CancellationToken cancellationToken)
    {
        var articles = _articleRepository.GetAllWithRelation(art => art.Authors, art => art.Categories);

        return _mapper.Map<IEnumerable<ArticleWithRelationsDTO>>(articles);
    }
}