using Chat_API.DTOs.Responses.Messages;
using Chat_API.Models;

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
}