namespace Chat_API.DTOs.Responses.Auth;

public class AuthResponse
{
    public string Token { get; init; } = null!;
    public long ExpiresIn { get; init; }
    public string? RefreshToken { get; init; } = null!;
}