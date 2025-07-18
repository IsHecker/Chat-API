using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat_API.Hubs;

[Authorize]
public class NotificationHub : Hub<INotificationClient>;