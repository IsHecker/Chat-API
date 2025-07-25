using Chat_API.Data.Repositories;
using Chat_API.DTOs.Requests.Common;
using Chat_API.DTOs.Responses.Conversations;
using Chat_API.Mappers;
using Chat_API.Models.Enums;
using Chat_API.Results;

namespace Chat_API.Services;

public class ConversationService
{
    private readonly IndividualConversationRepository _individualRepository;
    private readonly GroupConversationRepository _groupRepository;
    private readonly GroupService _groupService;
    private readonly IndividualConversationService _individualService;

    public ConversationService(
        GroupConversationRepository groupRepository,
        IndividualConversationRepository individualRepository,
        GroupService groupService,
        IndividualConversationService individualService)
    {
        _groupRepository = groupRepository;
        _individualRepository = individualRepository;
        _groupService = groupService;
        _individualService = individualService;
    }

    public async Task<IEnumerable<ConversationResponse>> ListAllConversationsAsync(Guid userId, Pagination? pagination = null)
    {
        pagination ??= Pagination.Default;

        var individualConversations = await _individualService.GetAllByUserIdAsync(userId, pagination);
        var groupConversations = await _groupService.GetAllByUserIdAsync(userId, pagination);

        return Enumerable.Concat(
            individualConversations,
            groupConversations);
    }

    public async Task<Result<IEnumerable<Guid>>> ListMemberIdsAsync(Guid conversationId, ConversationType type)
    {
        if (type == ConversationType.Group)
        {
            if (!await _groupRepository.ExistsAsync(conversationId))
                return Error.NotFound(description: "This Group doesn't exist.");


            return (await _groupRepository.ListGroupMemberIdsAsync(conversationId)).ToResult();
        }

        var conversation = await _individualRepository.GetByIdAsync(conversationId);
        if (conversation is null)
            return Error.NotFound(description: "This Individual Chat doesn't exist.");

        return new List<Guid> { conversation.User1Id, conversation.User2Id };
    }

    public async Task UpdateConversationLastActivity(Guid conversationId, ConversationType type)
    {
        if (type == ConversationType.Group)
        {
            await _groupRepository.UpdateLastActivityAsync(conversationId);
            return;
        }

        await _individualRepository.UpdateLastActivityAsync(conversationId);
    }
}