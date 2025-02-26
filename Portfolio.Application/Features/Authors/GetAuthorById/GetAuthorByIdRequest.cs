using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Authors.GetAuthorById;

public class GetAuthorByIdRequest : IRequest<AuthorDTO>
{
    public int Id { get; set; }
}