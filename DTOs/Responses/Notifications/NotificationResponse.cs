using Chat_API.Models.Enums;

namespace Chat_API.DTOs.Responses.Notifications;

public class NotificationResponse
{
    public NotificationType Type { get; init; }
    public Guid? SourceId { get; init; }
    public DateTime CreatedAt { get; init; }
    public object? Data { get; init; } = null!;
}