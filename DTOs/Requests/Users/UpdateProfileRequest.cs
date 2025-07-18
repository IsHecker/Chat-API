namespace Chat_API.DTOs.Requests.Users;

public class UpdateProfileRequest
{
    public string? Username { get; init; } = null!;
    public string? ProfilePictureUrl { get; init; } = null!;
}