namespace Chat_API.Models;

public class GroupConversation : Entity
{
    public string Name { get; init; } = null!;

    public string? GroupPictureUrl { get; init; } = null!;

    public ICollection<User> Members { get; init; } = [];

    public ICollection<User> Admins { get; init; } = [];

    /// <summary>
    /// Optional: Timestamp of the last activity in this group conversation.
    /// Can be used for sorting conversation lists without fetching all messages.
    /// </summary> 
    public DateTime? LastActivityAt { get; init; } = DateTime.UtcNow;
}