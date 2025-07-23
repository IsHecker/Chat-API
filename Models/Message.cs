using Chat_API.Models.Enums;

namespace Chat_API.Models;

public class Message : Entity
{
    public Guid SenderId { get; init; }
    public string Content { get; init; } = null!;
    public MessageStatus Status { get; init; }
    public Guid ConversationId { get; init; }
    public DateTime SentAt { get; init; } = DateTime.UtcNow;
}