using Chat_API.Models.Enums;

namespace Chat_API.Models;

public class FriendRequest : Entity
{
    public Guid SenderId { get; init; }
    public Guid ReceiverId { get; init; }
    public FriendRequestStatus Status { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public void SetStatus(FriendRequestStatus newStatus) => Status = newStatus;
}