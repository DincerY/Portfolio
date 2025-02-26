using AutoMapper;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Articles.GetArticles;

public class GetArticleHandler : IRequestHandler<GetArticlesRequest,IEnumerable<ArticleDTO>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetArticleHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ArticleDTO>> Handle(GetArticlesRequest request, CancellationToken cancellationToken)
    {
        var articles = _articleRepository.GetAll();
        return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
    }
}