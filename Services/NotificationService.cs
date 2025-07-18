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

    public async Task SendAsync<TEntity>(
        TEntity entityToSend,
        Guid senderId,
        Guid receiverId,
        NotificationType type,
        object? data = null
        ) where TEntity : notnull
    {
        if (entityToSend is null)
            throw new ArgumentNullException(paramName: nameof(entityToSend), message: "can't send a null entity.");

        await _context.AddAsync(entityToSend);

        var notification = new Notification
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            Type = type,
            SourceId = (entityToSend as Entity)!.Id,
            Data = data != null ? JsonSerializer.Serialize(data) : null,
            IsRead = false
        };

        await _context.AddAsync(notification);
        await _context.SaveChangesAsync();

        await _hubContext.Clients.User(receiverId.ToString()).ReceiveNotification(notification.ToResponse());
    }
}