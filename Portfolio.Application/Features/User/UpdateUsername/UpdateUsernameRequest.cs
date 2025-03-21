using MediatR;

namespace Portfolio.Application.Features.User.UpdateUser;

public class UpdateUsernameRequest : IRequest<UpdateUsernameResponse>
{
    public string PrevUsername { get; set; }
    public string NewUsername { get; set; }
}