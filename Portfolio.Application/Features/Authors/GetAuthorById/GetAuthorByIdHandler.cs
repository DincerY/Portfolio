using AutoMapper;
using MediatR;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Application.Interfaces.Repositories;

namespace Portfolio.Application.Features.Authors.GetAuthorById;

public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdRequest, GetAuthorByIdResponse>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public GetAuthorByIdHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<GetAuthorByIdResponse> Handle(GetAuthorByIdRequest request, CancellationToken cancellationToken)
    {
        var author = _authorRepository.GetById(request.Id);
        if (author == null)
        {
            throw new NotFoundException("There is no author in the entered id");
        }

        return _mapper.Map<GetAuthorByIdResponse>(author);
    }
}