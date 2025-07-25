using Chat_API.DTOs.Responses.Conversations;
using Chat_API.Models;
using Chat_API.Models.Enums;

namespace Chat_API.Mappers;

public static class ConversationMapper
{
    public static ConversationResponse ToResponse(this IndividualConversation conversation)
    {
        return new ConversationResponse
        {
            ConversationId = conversation.Id,
            Type = ConversationType.Individual.ToString(),
            LastActivityAt = conversation.LastActivityAt!.Value
        };
    }
    public static ConversationResponse ToResponse(this IndividualConversation conversation, User friend)
    {
        var response = conversation.ToResponse();
        response.Friend = friend.ToResponse();
        return response;
    }

    public static IEnumerable<ConversationResponse> ToResponse(
        this IEnumerable<IndividualConversation> conversations,
        IEnumerable<User> friends)
    {
        return conversations.Select(conv => conv.ToResponse(friends.First(f => f.Id == conv.FriendId)));
    }

    public static ConversationResponse ToResponse(this GroupConversation conversation)
    {
        return new ConversationResponse
        {
            ConversationId = conversation.Id,
            Type = ConversationType.Group.ToString(),
            Name = conversation.Name,
            GroupPictureUrl = conversation.GroupPictureUrl,
            LastActivityAt = conversation.LastActivityAt!.Value
        };
    }

    public static IEnumerable<ConversationResponse> ToResponse(this IEnumerable<GroupConversation> conversations)
    {
        return conversations.Select(ToResponse);
    }
}