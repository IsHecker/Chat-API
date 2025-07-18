namespace Chat_API.DTOs.Requests.Auth;

public class RegisterRequest
{
    public string Username { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string Role { get; init; } = null!;
}