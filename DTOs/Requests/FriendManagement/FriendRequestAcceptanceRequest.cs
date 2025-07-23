using Chat_API.Models.Enums;

namespace Chat_API.DTOs.Requests.FriendManagement;

public class FriendRequestAcceptanceRequest
{
    public string Status { get; init; } = null!;
}