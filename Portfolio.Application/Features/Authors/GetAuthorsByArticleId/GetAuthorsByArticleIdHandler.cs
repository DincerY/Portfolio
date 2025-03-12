using AutoMapper;
using MediatR;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Application.Interfaces.Repositories;

namespace Portfolio.Application.Features.Authors.GetAuthorsByArticleId;

public class GetAuthorsByArticleIdHandler : IRequestHandler<GetAuthorsByArticleIdRequest, IEnumerable<GetAuthorsByArticleIdResponse>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public GetAuthorsByArticleIdHandler(IMapper mapper, IAuthorRepository authorRepository)
    {
        _mapper = mapper;
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<GetAuthorsByArticleIdResponse>> Handle(GetAuthorsByArticleIdRequest request, CancellationToken cancellationToken)
    {
        var authors = _authorRepository.GetWhere(aut => aut.ArticleAuthors.Any(aa => aa.AuthorId == aut.Id && aa.ArticleId == request.Id));
        if (authors == null)
        {
            throw new NotFoundException("There is no article in the entered id");
        }
        return _mapper.Map<List<GetAuthorsByArticleIdResponse>>(authors);
    }
}