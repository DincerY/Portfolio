using AutoMapper;
using MediatR;
using Portfolio.Application.Services;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Articles.GetArticles;

public class GetArticlesHandler : IRequestHandler<GetArticlesRequest,IEnumerable<GetArticlesResponse>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public GetArticlesHandler(IArticleRepository articleRepository, IMapper mapper, ICacheService cacheService)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<GetArticlesResponse>> Handle(GetArticlesRequest request, CancellationToken cancellationToken)
    {
        var cache =await _cacheService.GetAsync<IEnumerable<Article>>("articles");
        if (cache != null)
        {
            return await _cacheService.GetAsync<IEnumerable<GetArticlesResponse>>("articles");
        }
       
        var articles = _articleRepository.GetAll();


        await _cacheService.SetAsync("articles", articles,TimeSpan.FromMinutes(10));
        
        return _mapper.Map<IEnumerable<GetArticlesResponse>>(articles);
    }
}