namespace Chat_API.Services.Auth.Models;

public class Credentials
{
    public string? Username { get; init; }
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string? Role { get; init; }
}