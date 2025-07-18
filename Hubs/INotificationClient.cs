namespace Chat_API.Hubs;

public interface INotificationClient
{
    Task ReceiveNotification(object notification);
}