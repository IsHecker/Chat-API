namespace Chat_API.Models.Joins;

public class GroupMember
{
    public Guid MembersId { get; init; }
    public Guid GroupConversationId { get; init; }
}