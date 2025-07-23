namespace Chat_API.DTOs.Responses.Users;

public class UserResponse
{
    public Guid Id { get; init; }
    public string Username { get; init; } = null!;
    public string? ProfilePictureUrl { get; init; } = null!;
    public string? Role { get; init; } = null!;
}