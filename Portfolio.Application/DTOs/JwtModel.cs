namespace Portfolio.Application.DTOs;

public class JwtModel
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
}