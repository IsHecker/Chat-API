using Chat_API.Hubs.DTOs;

namespace Chat_API.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(MessageDto message);
}