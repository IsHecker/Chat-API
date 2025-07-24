namespace Chat_API.Hubs.DTOs;

public class MessageDto
{
    public Guid MessageId { get; init; }
    public Guid ConversationId { get; init; }
    public Guid SenderId { get; init; }
    public string Type { get; init; } = null!;
    public string Content { get; init; } = null!;
    public DateTime SentAt { get; init; }
}