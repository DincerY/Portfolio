using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Features.Categories.GetArticlesByCategoryId;

public class GetArticlesByCategoryIdRequest : IRequest<IEnumerable<GetArticlesByCategoryIdResponse>>
{
    public int Id { get; set; }
}