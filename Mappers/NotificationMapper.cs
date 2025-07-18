using Chat_API.DTOs.Responses.Notifications;
using Chat_API.Models;

namespace Chat_API.Mappers;

public static class NotificationMapper
{
    public static NotificationResponse ToResponse(this Notification notification)
    {
        return new NotificationResponse
        {
            SenderId = notification.SenderId,
            ReceiverId = notification.ReceiverId,
            Type = notification.Type,
            SourceId = notification.SourceId,
            Data = notification.Data,
            CreatedAt = notification.CreatedAt
        };
    }
}