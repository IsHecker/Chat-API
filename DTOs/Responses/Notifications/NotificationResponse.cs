using Chat_API.Models.Enums;

namespace Chat_API.DTOs.Responses.Notifications;

public class NotificationResponse
{
    public Guid SenderId { get; init; }
    public Guid ReceiverId { get; init; }
    public NotificationType Type { get; init; }
    public Guid? SourceId { get; init; }
    public DateTime CreatedAt { get; init; }
    public object? Data { get; init; } = null!;
}