using MediatR;

namespace Portfolio.Application.Features.Authors.CreateAuthor;

public class CreateAuthorRequest : IRequest<CreateAuthorResponse>
{
    public string Name { get; set; }
    public string Surname { get; set; }
}