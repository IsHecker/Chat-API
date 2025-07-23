using Chat_API.DTOs.Responses.Notifications;
using Chat_API.Models;

namespace Chat_API.Mappers;

public static class NotificationMapper
{
    public static NotificationResponse ToResponse(this Notification notification, object? data)
    {
        return new NotificationResponse
        {
            Type = notification.Type,
            SourceId = notification.SourceId,
            Data = data,
            CreatedAt = notification.CreatedAt
        };
    }
}