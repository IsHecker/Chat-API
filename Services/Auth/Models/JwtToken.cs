namespace Chat_API.Services.Auth.Models;

public class JwtToken
{
    public string Token { get; init; } = null!;
    public long ExpiresIn { get; init; }
    public string? RefreshToken { get; init; } = null!;
}