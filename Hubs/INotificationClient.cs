namespace Chat_API.Hubs;

public interface INotificationClient
{
    Task ReceiveNotifications(object notification);
}