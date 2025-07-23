using Chat_API.Data.Interfaces;
using Chat_API.Data.Repositories;
using Chat_API.DTOs.Responses.FriendManagement;
using Chat_API.Models;
using Chat_API.Models.Enums;
using Chat_API.Results;
using Microsoft.AspNetCore.Identity;

namespace Chat_API.Services;

public class FriendRequestService
{
    private readonly FriendRequestRepository _friendRequestRepository;
    private readonly FriendshipRepository _friendshipRepository;
    private readonly IndividualConversationRepository _individualConversationRepository;
    private readonly NotificationService _notificationService;
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public FriendRequestService(
        FriendRequestRepository requestRepository,
        NotificationService notificationService,
        FriendshipRepository friendshipRepository,
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IndividualConversationRepository individualConversationRepository)
    {
        _friendRequestRepository = requestRepository;
        _notificationService = notificationService;
        _friendshipRepository = friendshipRepository;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _individualConversationRepository = individualConversationRepository;
    }

    public async Task<Result> SendRequestAsync(Guid userId, Guid receiverId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            throw new InvalidOperationException("User ID not found.");

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

        await _friendRequestRepository.AddAsync(friendRequest);

        // Push notification here
        await _notificationService.SendAsync(
            receiverId,
            NotificationType.FriendRequest,
            friendRequest.Id,
            new
            {
                FriendRequestId = friendRequest.Id,
                UserId = userId,
                Username = user.UserName,
                user.ProfilePictureUrl
            });

        return Result.Success;
    }

    public async Task<Result> AcceptRequest(Guid friendRequestId, Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            throw new InvalidOperationException("User ID not found.");

        var friendRequest = await _friendRequestRepository.GetByIdAsync(friendRequestId);
        if (friendRequest == null || friendRequest.Status != FriendRequestStatus.Pending)
            return Error.Validation(description: "Invalid Friend request.");

        if (friendRequest.ReceiverId != userId)
            return Error.Validation(description: "Not authorized.");

        friendRequest.SetStatus(FriendRequestStatus.Accepted);

        var friendship = new Friendship
        {
            User1Id = userId,
            User2Id = friendRequest.SenderId
        };

        var individualConversation = new IndividualConversation
        {
            User1Id = userId,
            User2Id = friendRequest.SenderId
        };

        await _friendRequestRepository.UpdateAsync(friendRequest);
        await _friendshipRepository.AddAsync(friendship);
        await _individualConversationRepository.AddAsync(individualConversation);

        // Push notification here
        await _notificationService.SendAsync(
            receiverId: friendRequest.SenderId,
            NotificationType.FriendRequestAccepted,
            friendRequest.Id,
            new { UserId = userId, Username = user.UserName, user.ProfilePictureUrl });

        return Result.Success;
    }

    // Additional methods: DeclineRequest, CancelRequest, Unfriend, etc.
}