using MediatR;

namespace Portfolio.Application.Features.Authors.GetAuthorById;

public class GetAuthorByIdRequest : IRequest<GetAuthorByIdResponse>
{
    public int Id { get; set; }
}