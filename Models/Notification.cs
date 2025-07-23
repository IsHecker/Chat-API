using Chat_API.Models.Enums;

namespace Chat_API.Models;

public class Notification : Entity
{
    public Guid ReceiverId { get; init; }
    public NotificationType Type { get; init; }
    public Guid? SourceId { get; init; }
    public string? Data { get; init; }
    public bool IsRead { get; init; } = false;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}