using MediatR;

namespace Portfolio.Application.Features.Auth.Refresh;

public class RefreshRequest : IRequest<RefreshResponse>
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}