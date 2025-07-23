namespace Chat_API.Hubs;

public interface IMessageClient
{
    Task ReceiveMessage(Guid userId, string content);
}