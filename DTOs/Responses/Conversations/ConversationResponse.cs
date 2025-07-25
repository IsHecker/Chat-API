using Chat_API.DTOs.Responses.Users;

namespace Chat_API.DTOs.Responses.Conversations;

public class ConversationResponse
{
    public Guid ConversationId { get; init; }
    public string Type { get; init; } = null!;

    public UserResponse? Friend { get; set; } = null!;

    public string? Name { get; init; } = null!;
    public string? GroupPictureUrl { get; init; } = null!;
    public IEnumerable<UserResponse>? Members { get; set; } = null!;
    public DateTime LastActivityAt { get; init; }
}