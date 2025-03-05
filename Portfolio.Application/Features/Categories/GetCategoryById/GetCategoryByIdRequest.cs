using MediatR;


namespace Portfolio.Application.Features.Categories.GetCategoryById;

public class GetCategoryByIdRequest : IRequest<GetCategoryByIdResponse>
{
    public int Id { get; set; }
}