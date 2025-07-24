using Chat_API.Data.Interfaces;
using Chat_API.Data.Repositories;
using Chat_API.Mappers;
using Chat_API.Models;
using Chat_API.Models.Enums;
using Chat_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat_API.Hubs;

[Authorize]
public class ChatHub : Hub<IChatClient>
{
    private readonly OnlineUserStore _onlineUserStore;
    private readonly ConversationService _conversationService;
    private readonly MessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChatHub(
        ConversationService conversationService,
        MessageRepository messageRepository,
        IUnitOfWork unitOfWork,
        OnlineUserStore onlineUserStore)
    {
        _conversationService = conversationService;
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
        _onlineUserStore = onlineUserStore;
    }

    public override Task OnConnectedAsync()
    {
        _onlineUserStore.AddUser(Guid.Parse(Context.UserIdentifier!));
        return base.OnConnectedAsync();
    }

    public async Task SendMessage(Guid conversationId, string conversationType, string content)
    {
        var userId = Guid.Parse(Context.UserIdentifier ?? throw new HubException("Unauthorized"));

        var type = Enum.Parse<ConversationType>(conversationType, true);
        var listResult = await _conversationService.ListMemberIdsAsync(conversationId, type);
        if (listResult.IsError)
            throw new HubException(listResult.Errors.First().Description);

        var memberIds = listResult.Value;
        if (!memberIds.Contains(userId))
            throw new HubException("User not part of this conversation.");

        var message = new Message
        {
            ConversationId = conversationId,
            SenderId = userId,
            Content = content,
            Status = MessageStatus.Unsent,
            SentAt = DateTime.UtcNow
        };
        await _messageRepository.AddAsync(message);
        await _unitOfWork.SaveChangesAsync();
        await _conversationService.UpdateConversationLastActivity(conversationId, type);

        var messageDto = message.ToMessageDto(type);

        foreach (var memberId in memberIds)
        {
            if (memberId == userId)
                continue;

            await Clients.User(memberId.ToString()).ReceiveMessage(messageDto);
        }
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _onlineUserStore.RemoveUser(Guid.Parse(Context.UserIdentifier!));
        return base.OnDisconnectedAsync(exception);
    }
}