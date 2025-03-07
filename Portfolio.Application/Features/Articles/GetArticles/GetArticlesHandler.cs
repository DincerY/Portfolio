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

    public GetArticlesHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetArticlesResponse>> Handle(GetArticlesRequest request, CancellationToken cancellationToken)
    {
        var articles = _articleRepository.GetAllWithPagination(request.PageSize,request.PageNumber).ToList();
        return _mapper.Map<IEnumerable<GetArticlesResponse>>(articles);
    }
}