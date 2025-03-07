using AutoMapper;
using MediatR;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Authors.GetAuthors;

public class GetAuthorsHandler : IRequestHandler<GetAuthorsRequest,IEnumerable<GetAuthorsResponse>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public GetAuthorsHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAuthorsResponse>> Handle(GetAuthorsRequest request, CancellationToken cancellationToken)
    {
        /*var authors = _authorRepository.GetAllWithRelation(aut => aut.Articles);*/
        var authors = _authorRepository.GetAllWithPagination(request.PageSize,request.PageNumber);
        return _mapper.Map<IEnumerable<GetAuthorsResponse>>(authors);
    }
}