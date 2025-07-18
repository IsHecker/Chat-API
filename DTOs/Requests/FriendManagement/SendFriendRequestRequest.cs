namespace Chat_API.DTOs.Requests.FriendManagement;

public class SendFriendRequestRequest
{
    public Guid RecipientUserId { get; init; }
}