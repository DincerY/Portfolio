using AutoMapper;
using MediatR;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Authors.GetAuthorsByIds;

public class GetAuthorsByIdsHandler : IRequestHandler<GetAuthorsByIdsRequest,IEnumerable<GetAuthorsByIdsResponse>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public GetAuthorsByIdsHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAuthorsByIdsResponse>> Handle(GetAuthorsByIdsRequest request, CancellationToken cancellationToken)
    {
        var authors = _authorRepository.GetWhere(aut => request.Ids.Contains(aut.Id)).ToList();

        if (authors.Count != request.Ids.Count)
        {
            throw new NotFoundException("There is no author in the entered ids");
        }

        return _mapper.Map<IEnumerable<GetAuthorsByIdsResponse>>(authors);
    }
}