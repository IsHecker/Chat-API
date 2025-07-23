namespace Chat_API.Models.Joins;

public class GroupAdmin
{
    public Guid AdminsId { get; init; }
    public Guid GroupConversationId { get; init; }
}