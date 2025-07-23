namespace Chat_API.DTOs.Requests.Groups;

public class CreateGroupRequest
{
    public string Name { get; init; } = null!;
    public List<Guid> MemberIds { get; init; } = null!;
    public string? GroupPictureUrl { get; init; } = null!;
}