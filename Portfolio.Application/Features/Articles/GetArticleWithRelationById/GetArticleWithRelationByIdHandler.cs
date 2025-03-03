using AutoMapper;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Articles.GetArticleWithRelationById;

public class GetArticleWithRelationByIdHandler : IRequestHandler<GetArticleWithRelationByIdRequest,ArticleWithRelationsDTO>
{
    //TODO : Buarada neden articleRepo kullandık diğerlerinde kullanmadık
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetArticleWithRelationByIdHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }


    public async Task<ArticleWithRelationsDTO> Handle(GetArticleWithRelationByIdRequest request, CancellationToken cancellationToken)
    {
        var article = _articleRepository.GetByIdWithRelation(request.Id, art => art.Authors, art => art.Categories).First();

        if (article == null)
        {
            throw new NotFoundException("There is no article in the entered id");
        }
        return _mapper.Map<ArticleWithRelationsDTO>(article);
    }
}