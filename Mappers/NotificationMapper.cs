using System.Text.Json;
using Chat_API.DTOs.Responses.Notifications;
using Chat_API.Models;

namespace Chat_API.Mappers;

public static class NotificationMapper
{
    public static NotificationResponse ToResponse(this Notification notification, object? data)
    {
        return new NotificationResponse
        {
            Id = notification.Id,
            Type = notification.Type,
            SourceId = notification.SourceId,
            Data = data is string str ? JsonSerializer.Deserialize<object>(str) : data,
            CreatedAt = notification.CreatedAt
        };
    }

    public static IEnumerable<NotificationResponse> ToResponse(this IEnumerable<Notification> notifications)
    {
        return notifications.Select(n => n.ToResponse(n.Data));
    }
}