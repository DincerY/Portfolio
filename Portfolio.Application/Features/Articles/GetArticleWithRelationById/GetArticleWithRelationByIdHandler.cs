using AutoMapper;
using MediatR;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Application.Interfaces.Repositories;

namespace Portfolio.Application.Features.Articles.GetArticleWithRelationById;

public class GetArticleWithRelationByIdHandler : IRequestHandler<GetArticleWithRelationByIdRequest, GetArticleWithRelationByIdResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetArticleWithRelationByIdHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }


    public async Task<GetArticleWithRelationByIdResponse> Handle(GetArticleWithRelationByIdRequest request, CancellationToken cancellationToken)
    {
        var article = _articleRepository.GetByIdWithRelation(request.Id, art => art.Authors, art => art.Categories);

        if (article == null)
        {
            throw new NotFoundException("There is no article in the entered id");
        }
        return _mapper.Map<GetArticleWithRelationByIdResponse>(article);
    }
}