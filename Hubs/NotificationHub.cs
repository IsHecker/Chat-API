using Chat_API.Models.Enums;
using Chat_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat_API.Hubs;

[Authorize]
public class NotificationHub : Hub<INotificationClient>
{
    private readonly NotificationService _notificationService;

    public NotificationHub(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public override async Task OnConnectedAsync()
    {
        await _notificationService.DeliverLatestNotificationsAsync(Guid.Parse(Context.UserIdentifier!));
        await base.OnConnectedAsync();
    }

    public async Task UpdateNotificationStatus(IEnumerable<Guid> notificationIds, string status)
    {
        if (!Enum.TryParse<NotificationStatus>(status, true, out var notificationStatus))
            return;

        await _notificationService.UpdateNotificationStatus(notificationIds, notificationStatus);
    }
}