using AutoMapper;
using MediatR;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Entities;
using Portfolio.Application.Interfaces.Repositories;

namespace Portfolio.Application.Features.Authors.CreateAuthor;

public class CreateAuthorHandler : IRequestHandler<CreateAuthorRequest,CreateAuthorResponse>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    public CreateAuthorHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<CreateAuthorResponse> Handle(CreateAuthorRequest request, CancellationToken cancellationToken)
    {
        bool isAuthorExist = _authorRepository.IsExists(aut => aut.Name.ToLower() == request.Name.ToLower());
        if (isAuthorExist)
        {
            throw new BusinessException("Author's name is already exist");
        }

        Author author = _mapper.Map<Author>(request);
        var addedAuthor = _authorRepository.Add(author);
        if (addedAuthor != null)
        {
            return new CreateAuthorResponse()
            {
                Id = addedAuthor.Id,
                Name = addedAuthor.Name,
                CreatedDate = author.PublishedDate
            };
        }
        else
        {
            throw new BusinessException("Adding is not successful");
        }
    }
}