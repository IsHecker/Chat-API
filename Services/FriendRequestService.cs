using Chat_API.Data.Interfaces;
using Chat_API.Data.Repositories;
using Chat_API.DTOs.Responses.FriendManagement;
using Chat_API.Models;
using Chat_API.Models.Enums;
using Chat_API.Results;

namespace Chat_API.Services;

public class FriendRequestService
{
    private readonly FriendRequestRepository _friendRequestRepository;
    private readonly FriendshipRepository _friendshipRepository;
    private readonly NotificationService _notificationService;
    private readonly IUnitOfWork _unitOfWork;

    public FriendRequestService(
        FriendRequestRepository requestRepository,
        NotificationService notificationService,
        FriendshipRepository friendshipRepository,
        IUnitOfWork unitOfWork)
    {
        _friendRequestRepository = requestRepository;
        _notificationService = notificationService;
        _friendshipRepository = friendshipRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SendFriendRequestResponse>> SendRequestAsync(Guid userId, Guid receiverId)
    {
        // if (await AlreadyFriends(senderId, receiverId))
        //     return Error.Validation(description: "Already friends.");

        // if (await RequestExists(senderId, receiverId))
        //     return Error.Validation(description: "Request already sent.");

        // if (await RequestExists(receiverId, senderId))
        // {
        //     await AcceptMutualRequest(receiverId, senderId);
        //     return Result.Success;
        // }

        // if (await IsBlocked(senderId, receiverId))
        //     return Error.Validation(description: "Cannot send request to this user.");

        var friendRequest = new FriendRequest
        {
            SenderId = userId,
            ReceiverId = receiverId,
            Status = FriendRequestStatus.Pending
        };

        // Push notification here
        await _notificationService.SendAsync(
            friendRequest,
            userId,
            receiverId,
            NotificationType.FriendRequest,
            new { RequestId = friendRequest.Id });

        return new SendFriendRequestResponse { RequestId = friendRequest.Id };
    }

    public async Task<Result> AcceptRequest(Guid requestId, Guid userId, Guid receiverId)
    {
        var friendRequest = await _friendRequestRepository.GetByIdAsync(requestId);
        if (friendRequest == null || friendRequest.Status != FriendRequestStatus.Pending)
            return Error.Validation(description: "Invalid Friend request.");

        if (friendRequest.ReceiverId != userId)
            return Error.Validation(description: "Not authorized.");

        friendRequest.SetStatus(FriendRequestStatus.Accepted);

        var friendship = new Friendship
        {
            User1Id = friendRequest.SenderId,
            User2Id = friendRequest.ReceiverId
        };

        await _friendshipRepository.AddAsync(friendship);
        await _friendRequestRepository.DeleteAsync(friendRequest);
        await _unitOfWork.SaveChangesAsync();

        // Push notification here
        await _notificationService.SendAsync(
            friendRequest,
            userId,
            receiverId,
            NotificationType.FriendRequestAccepted);

        return Result.Success;
    }

    // Additional methods: DeclineRequest, CancelRequest, Unfriend, etc.
}