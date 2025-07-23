namespace Chat_API.Models;

public class IndividualConversation : Entity
{
    public Guid User1Id { get; init; }
    public Guid User2Id { get; init; }

    public bool IsBlocked { get; init; } = false;
    public DateTime? LastActivityAt { get; init; } = DateTime.UtcNow;

    public Guid FriendId => User1Id == SharedData.UserId ? User2Id : User1Id;
}