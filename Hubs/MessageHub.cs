using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat_API.Hubs;

[Authorize]
public class MessageHub : Hub<IMessageClient>
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public async Task SendMessage(Guid userId, string content, IEnumerable<Guid> receiverIds)
    {

    }
}