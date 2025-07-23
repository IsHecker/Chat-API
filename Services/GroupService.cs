using Chat_API.Data.Interfaces;
using Chat_API.Data.Repositories;
using Chat_API.DTOs.Requests.Common;
using Chat_API.DTOs.Requests.Groups;
using Chat_API.DTOs.Responses.Conversations;
using Chat_API.DTOs.Responses.Users;
using Chat_API.Mappers;
using Chat_API.Models;
using Chat_API.Results;

namespace Chat_API.Services;

public class GroupService
{
    private readonly GroupConversationRepository _groupRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GroupService(GroupConversationRepository groupRepository, IUnitOfWork unitOfWork)
    {
        _groupRepository = groupRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ConversationResponse>> GetAllByUserIdAsync(Guid userId, Pagination pagination)
    {
        var groups = (await _groupRepository.GetAllByUserIdAsync(userId, pagination)).ToList();

        return groups.Select(group =>
        {
            var response = group.ToResponse();
            response.Members = ListGroupMembersAsync(group.Id, pagination).GetAwaiter().GetResult().Value;
            return response;
        });
    }

    public async Task<Result> CreateNewGroupAsync(Guid userId, CreateGroupRequest newGroup)
    {
        if (!newGroup.MemberIds.Contains(userId))
            return Error.Validation(description: "Group Members must contain the Creator.");

        if (newGroup.MemberIds.Count < 2)
            return Error.Validation(description: "Group must have at least 2 Members.");

        var groupConversation = new GroupConversation
        {
            Name = newGroup.Name,
            GroupPictureUrl = newGroup.GroupPictureUrl,
        };

        await _groupRepository.AddAsync(groupConversation);
        await _groupRepository.AddMemberAsync(groupConversation.Id, newGroup.MemberIds);
        await _groupRepository.AddAdminAsync(groupConversation.Id, userId);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success;
    }

    public async Task<Result<ConversationResponse>> GetGroupDetailsAsync(Guid conversationId)
    {
        var groupConversation = await _groupRepository.GetByIdAsync(conversationId);
        if (groupConversation is null)
            return Error.NotFound(description: "This group doesn't exist.");


        var response = groupConversation.ToResponse();
        response.Members = (await ListGroupMembersAsync(conversationId, Pagination.Default)).Value;

        return response;
    }

    public async Task<Result<IEnumerable<UserResponse>>> ListGroupMembersAsync(Guid conversationId, Pagination pagination)
    {
        var members = await _groupRepository.ListGroupMembersAsync(conversationId, pagination);
        if (members is null)
            return Error.NotFound(description: "This group doesn't exist.");

        var adminIds = (await _groupRepository.ListGroupAdminIdsAsync(conversationId)).ToHashSet();
        if (adminIds.Count < 1)
            throw new InvalidOperationException(message: "Can not have a group without admins.");

        return members.Select(m => m.ToResponse(adminIds.Contains(m.Id) ? "admin" : "member")).ToResult();
    }

    public async Task<Result> AddMembersAsync(Guid conversationId, IEnumerable<Guid> memberIds)
    {
        if (await _groupRepository.IsMemberExistAsync(conversationId, memberIds))
            return Error.Conflict(description: "One or more members already exist");

        await _groupRepository.AddMemberAsync(conversationId, memberIds);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success;
    }

    public async Task<Result> RemoveMemberAsync(Guid conversationId, Guid memberId)
    {
        if (!await _groupRepository.IsMemberExistAsync(conversationId, memberId))
            return Error.NotFound(description: "The specified member was not found in this group.");

        await _groupRepository.RemoveMemberAsync(conversationId, memberId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success;
    }

    public async Task<Result> UpdateGroupDetails(Guid conversationId, UpdateGroupDetailsRequest groupDetails)
    {
        await _groupRepository.UpdateDetails(conversationId, groupDetails.Name, groupDetails.GroupPictureUrl);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success;
    }
}