using Chat_API.DTOs.Responses.Conversations;
using Chat_API.Models;

namespace Chat_API.Mappers;

public static class ConversationMapper
{
    public static ConversationResponse ToResponse(this IndividualConversation conversation, User friend)
    {
        return new ConversationResponse
        {
            ConversationId = conversation.Id,
            Type = "individual",
            Friend = friend.ToResponse()
        };
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
            Type = "group",
            Name = conversation.Name,
            GroupPictureUrl = conversation.GroupPictureUrl
        };
    }

    public static IEnumerable<ConversationResponse> ToResponse(this IEnumerable<GroupConversation> conversations)
    {
        return conversations.Select(ToResponse);
    }
}