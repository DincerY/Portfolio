using AutoMapper;
using MediatR;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Application.Interfaces.Repositories;

namespace Portfolio.Application.Features.Articles.GetArticlesByCategoryId;

public class GetArticlesByCategoryIdHandler : IRequestHandler<GetArticlesByCategoryIdRequest,IEnumerable<GetArticlesByCategoryIdResponse>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetArticlesByCategoryIdHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetArticlesByCategoryIdResponse>> Handle(GetArticlesByCategoryIdRequest request, CancellationToken cancellationToken)
    {
        var articles = _articleRepository.GetWhere(art =>
            art.ArticleCategories.Any(ac => ac.ArticleId == art.Id && ac.CategoryId == request.Id));

        if (articles == null)
        {
            throw new NotFoundException("There is no category in the entered id");
        }

        return _mapper.Map<IEnumerable<GetArticlesByCategoryIdResponse>>(articles);
    }
}