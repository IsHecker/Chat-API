using Chat_API.DTOs.Responses.Messages;
using Chat_API.Hubs.DTOs;
using Chat_API.Models;
using Chat_API.Models.Enums;

namespace Chat_API.Mappers;

public static class MessageMapper
{
    public static MessageResponse ToResponse(this Message message)
    {
        return new MessageResponse
        {
            SenderId = message.SenderId,
            Content = message.Content,
            Status = message.Status.ToString(),
            SentAt = message.SentAt
        };
    }

    public static IEnumerable<MessageResponse> ToResponse(this IEnumerable<Message> messages)
    {
        return messages.Select(ToResponse);
    }

    public static MessageDto ToMessageDto(this Message message, ConversationType type)
    {
        return new MessageDto
        {
            MessageId = message.Id,
            ConversationId = message.ConversationId,
            SenderId = message.SenderId,
            Type = type.ToString(),
            Content = message.Content,
            SentAt = message.SentAt
        };
    }
}