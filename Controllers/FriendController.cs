using Chat_API.DTOs.Requests.FriendManagement;
using Chat_API.Models.Enums;
using Chat_API.Results;
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
    [HttpPatch(ApiEndpoints.FriendRequests.RespondToFriendRequest)]
    public async Task<IActionResult> RespondToFriendRequest(Guid requestId, [FromBody] RespondToFriendRequestRequest request)
    {
        if (!Enum.TryParse<FriendRequestStatus>(request.Status, true, out var status))
            return Problem(Error.Validation(description: "Invalid Friend Request Status."));

        var acceptanceResult = await _friendRequestService.RespondToRequestAsync(requestId, UserId, status);
        return acceptanceResult.Match(Ok, Problem);
    }
}