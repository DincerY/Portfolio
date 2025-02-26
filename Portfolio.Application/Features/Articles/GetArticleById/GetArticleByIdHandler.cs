using AutoMapper;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Articles.GetArticleById;

public class GetArticleByIdHandler : IRequestHandler<GetArticleByIdRequest,ArticleDTO>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetArticleByIdHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<ArticleDTO> Handle(GetArticleByIdRequest request, CancellationToken cancellationToken)
    {
        Article article = _articleRepository.GetById(request.Id);
        if (article == null)
        {
            throw new NotFoundException("There is no article in the entered id");
        }
        return _mapper.Map<ArticleDTO>(article);
    }
}