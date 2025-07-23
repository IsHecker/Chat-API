using Chat_API.Data.Interfaces;
using Chat_API.Data.Repositories;
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

        if (await _friendshipRepository.IsFriendsAsync(userId, receiverId))
            return Error.Validation(description: "Already friends.");

        if (await _friendRequestRepository.RequestExistsAsync(userId, receiverId))
            return Error.Validation(description: "Request already sent.");

        var friendRequestId = await _friendRequestRepository.GetFriendRequestId(receiverId, userId);
        if (friendRequestId is not null)
        {
            var acceptResult = await RespondToRequestAsync(friendRequestId.Value, userId, FriendRequestStatus.Accepted);

            if (acceptResult.IsError)
                return acceptResult.Errors;

            return Result.Success;
        }

        var friendRequest = new FriendRequest
        {
            SenderId = userId,
            ReceiverId = receiverId,
            Status = FriendRequestStatus.Pending
        };

        await _friendRequestRepository.AddAsync(friendRequest);

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

        await _unitOfWork.SaveChangesAsync();

        return Result.Success;
    }

    public async Task<Result> RespondToRequestAsync(Guid friendRequestId, Guid userId, FriendRequestStatus requestStatus)
    {
        if (requestStatus == FriendRequestStatus.Pending)
            return Error.Validation(description: "Can't respond to a Friend Request with 'Pending'.");

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            throw new InvalidOperationException("User ID not found.");

        var friendRequest = await _friendRequestRepository.GetByIdAsync(friendRequestId);
        if (friendRequest == null || friendRequest.Status != FriendRequestStatus.Pending)
            return Error.Validation(description: "Invalid Friend request.");

        if (friendRequest.ReceiverId != userId)
            return Error.Validation(description: "Not authorized.");

        friendRequest.SetStatus(requestStatus);

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

        await _notificationService.SendAsync(
            receiverId: friendRequest.SenderId,
            Enum.Parse<NotificationType>($"{nameof(FriendRequest)}{requestStatus}"),
            friendRequest.Id,
            new
            {
                UserId = userId,
                Username = user.UserName,
                user.ProfilePictureUrl
            });

        await _unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}