using System.Text.Json;
using Chat_API.Data;
using Chat_API.Hubs;
using Chat_API.Mappers;
using Chat_API.Models;
using Chat_API.Models.Enums;
using Microsoft.AspNetCore.SignalR;

namespace Chat_API.Services;

public class NotificationService
{
    private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;
    private readonly ApplicationDbContext _context;

    public NotificationService(
        ApplicationDbContext context,
        IHubContext<NotificationHub, INotificationClient> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    public async Task SendAsync(
        Guid receiverId,
        NotificationType type,
        Guid? entityId = null,
        object? data = null
        )
    {
        var notification = new Notification
        {
            ReceiverId = receiverId,
            Type = type,
            SourceId = entityId,
            Data = data != null ? JsonSerializer.Serialize(data) : null,
            IsRead = false
        };

        await _context.AddAsync(notification);
        await _context.SaveChangesAsync();

        await _hubContext.Clients.User(receiverId.ToString()).ReceiveNotification(notification.ToResponse(data));
    }
}