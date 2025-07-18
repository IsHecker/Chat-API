using Chat_API.Models.Enums;

namespace Chat_API.DTOs.Requests.FriendManagement;

public class FriendRequestAcceptanceRequest
{
    public Guid ReceiverId { get; init; }
    public string Status { get; init; }
}