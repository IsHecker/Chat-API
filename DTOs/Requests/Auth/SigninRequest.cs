namespace Chat_API.DTOs.Requests.Auth;

public class SigninRequest
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}