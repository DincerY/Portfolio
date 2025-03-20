namespace Portfolio.Application.Features.Auth.Login;

public class LoginResponse
{
    public bool AuthenticateResult { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime AccessTokenExpireDate { get; set; }
    public string Role { get; set; }
    public string Username { get; set; }
}