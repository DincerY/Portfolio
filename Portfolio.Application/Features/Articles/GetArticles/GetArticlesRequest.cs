using MediatR;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Features.Articles.GetArticles;

public class GetArticlesRequest : IRequest<IEnumerable<ArticleDTO>>
{

}