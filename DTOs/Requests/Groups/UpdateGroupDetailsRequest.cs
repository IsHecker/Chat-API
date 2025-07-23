namespace Chat_API.DTOs.Requests.Groups;

public class UpdateGroupDetailsRequest
{
    public string? Name { get; init; } = null!;
    public string? GroupPictureUrl { get; init; } = null!;
}