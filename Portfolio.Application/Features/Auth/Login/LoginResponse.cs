namespace Portfolio.Application.Features.Auth.Login;

public class LoginResponse
{
    public bool AuthenticateResult { get; set; }
    public string Token { get; set; }
    public DateTime AccessTokenExpireDate { get; set; }
}