using MediatR;

namespace Portfolio.Application.Features.Authors.GetAuthors;

public class GetAuthorsRequest : IRequest<IEnumerable<GetAuthorsResponse>>
{

}