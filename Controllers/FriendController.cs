using Chat_API.DTOs.Requests.FriendManagement;
using Chat_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat_API.Controllers;

public class FriendController : ApiController
{
    private readonly FriendRequestService _friendRequestService;

    public FriendController(FriendRequestService friendRequestService)
    {
        _friendRequestService = friendRequestService;
    }

    [Authorize]
    [HttpPost(ApiEndpoints.FriendRequests.SendRequest)]
    public async Task<IActionResult> SendFriendRequest(SendFriendRequestRequest request)
    {
        var requestResult = await _friendRequestService.SendRequestAsync(UserId, request.RecipientUserId);
        return requestResult.Match(Ok, Problem);
    }

    [Authorize]
    [HttpPatch(ApiEndpoints.FriendRequests.FriendRequestAcceptance)]
    public async Task<IActionResult> FriendRequestAcceptance(Guid requestId, [FromBody] FriendRequestAcceptanceRequest request)
    {
        var acceptanceResult = await _friendRequestService.AcceptRequest(requestId, UserId, request.ReceiverId);
        return acceptanceResult.Match(Ok, Problem);
    }
}