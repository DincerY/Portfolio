using MediatR;

namespace Portfolio.Application.Features.User.UpdateUser;

public class UpdateUserRequest : IRequest<UpdateUserResponse>
{
    public string PrevUsername { get; set; }
    public string NewUsername { get; set; }
    public string Password { get; set; }
    //Varsayılan olarak User
    public string Role { get; set; } = "User";
}