namespace Chat_API.DTOs.Responses.Users;

public class GetProfileResponse
{
    public Guid UserId { get; init; }
    public string Username { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Status { get; init; } = null!;
    public string? ProfilePictureUrl { get; init; } = null!;
}