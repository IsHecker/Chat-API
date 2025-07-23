using Chat_API.Data.Repositories;
using Chat_API.DTOs.Requests.Common;
using Chat_API.DTOs.Responses.Common;
using Chat_API.DTOs.Responses.Messages;
using Chat_API.Mappers;

namespace Chat_API.Services;

public class MessageService
{
    private readonly MessageRepository _messageRepository;

    public MessageService(MessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<PagedResponse<MessageResponse>> ListConversationMessagesAsync(Guid conversationId, Pagination pagination)
    {
        var messages = await _messageRepository.ListConversationMessagesAsync(conversationId, pagination);

        return messages.ToResponse().ToPagedResponse(pagination);
    }
}