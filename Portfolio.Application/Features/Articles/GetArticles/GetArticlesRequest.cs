using MediatR;


namespace Portfolio.Application.Features.Articles.GetArticles;

public class GetArticlesRequest : IRequest<IEnumerable<GetArticlesResponse>>
{

}