using System.Text.Json;
using Chat_API.Data;
using Chat_API.DTOs.Requests.Common;
using Chat_API.DTOs.Responses.Notifications;
using Chat_API.Hubs;
using Chat_API.Mappers;
using Chat_API.Models;
using Chat_API.Models.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

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

        await _hubContext.Clients.User(receiverId.ToString()).ReceiveNotifications(notification.ToResponse(data));
    }

    public async Task DeliverLatestNotificationsAsync(Guid userId)
    {
        var pendingNotificationsCount = _context.Notifications
                                .Where(n => n.ReceiverId == userId && n.Status == NotificationStatus.Unsent)
                                .Count();

        var notificationsResponse = (await ListNotificationsAsync(userId, Pagination.Default)).ToList();

        await _hubContext.Clients.User(userId.ToString())
            .ReceiveNotifications(new
            {
                Pending = pendingNotificationsCount,
                Notifications = notificationsResponse
            });
    }

    public async Task UpdateNotificationStatus(IEnumerable<Guid> notificationIds, NotificationStatus status)
    {
        await _context.Notifications.Where(n => notificationIds.Contains(n.Id)).ExecuteUpdateAsync(setter =>
                setter.SetProperty(p => p.Status, status)
            );
    }

    public Task<IEnumerable<NotificationResponse>> ListNotificationsAsync(Guid userId, Pagination pagination)
    {
        return Task.FromResult(_context.Notifications
                    .Where(n => n.ReceiverId == userId)
                    .OrderByDescending(n => n.CreatedAt)
                    .Paginate(pagination)
                    .ToResponse());
    }
}