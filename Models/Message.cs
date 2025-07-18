namespace Chat_API.Models;

public class Message : Entity
{
    public Guid SenderId { get; init; }
    public Guid ReceiverId { get; init; }
    public string Content { get; init; } = null!;
    public bool IsRead { get; init; }
    public Guid? ConversationId { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
}