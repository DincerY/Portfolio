using AutoMapper;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Application.Features.Authors.GetAuthors;

public class GetAuthorsHandler : IRequestHandler<GetAuthorsRequest,IEnumerable<AuthorDTO>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public GetAuthorsHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorDTO>> Handle(GetAuthorsRequest request, CancellationToken cancellationToken)
    {
        var authors = _authorRepository.GetAllWithRelation(aut => aut.Articles);
        return _mapper.Map<IEnumerable<AuthorDTO>>(authors);
    }
}