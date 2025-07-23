using Chat_API.Data;
using Chat_API.Data.Repositories;
using Chat_API.DTOs.Requests.Common;
using Chat_API.Mappers;
using Chat_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat_API.Controllers;

[Authorize]
public class ConversationController : ApiController
{
    private readonly IndividualConversationRepository _individualConversationRepository;
    private readonly GroupService _groupService;
    private readonly MessageService _messageService;
    private readonly ApplicationDbContext _dbContext;

    public ConversationController(
        IndividualConversationRepository oneOnOneConversationRepository,
        ApplicationDbContext dbContext,
        MessageService messageService,
        GroupService groupService)
    {
        _individualConversationRepository = oneOnOneConversationRepository;
        _dbContext = dbContext;
        _messageService = messageService;
        _groupService = groupService;
    }

    [HttpGet(ApiEndpoints.Conversations.ListAllConversations)]
    public async Task<IActionResult> ListAllConversations([FromQuery] Pagination pagination)
    {
        var individualConversations = await _individualConversationRepository.GetAllByUserIdAsync(UserId, pagination);
        var groupConversations = await _groupService.GetAllByUserIdAsync(UserId, pagination);

        var friends = _dbContext.Users
                .Where(u => individualConversations
                    .Any(conv => u.Id == (conv.User1Id == SharedData.UserId ? conv.User2Id : conv.User1Id)))
                .ToList();

        return Ok(Enumerable.Concat(
            individualConversations.ToResponse(friends),
            groupConversations)
            .ToPagedResponse(pagination));
    }

    [HttpGet(ApiEndpoints.Conversations.ListConversationMessages)]
    public async Task<IActionResult> ListConversationMessages(Guid conversationId, [FromQuery] Pagination pagination)
    {
        return Ok(await _messageService.ListConversationMessagesAsync(conversationId, pagination));
    }
}