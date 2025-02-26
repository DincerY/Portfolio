using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Authors.GetAuthors;

public class GetAuthorsRequest : IRequest<IEnumerable<AuthorDTO>>
{

}