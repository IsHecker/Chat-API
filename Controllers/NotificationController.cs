using Chat_API.DTOs.Requests.Common;
using Chat_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat_API.Controllers;

[Authorize]
public class NotificationController : ApiController
{
    private readonly NotificationService _notificationService;

    public NotificationController(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet(ApiEndpoints.Notifications.ListNotifications)]
    public async Task<IActionResult> ListNotifications([FromQuery] Pagination pagination)
    {
        return Ok(await _notificationService.ListNotificationsAsync(UserId, pagination));
    }
}