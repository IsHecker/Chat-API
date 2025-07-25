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
    private readonly ConversationService _conversationService;
    private readonly MessageService _messageService;

    public ConversationController(
        MessageService messageService,
        ConversationService conversationService)
    {
        _messageService = messageService;
        _conversationService = conversationService;
    }

    [HttpGet(ApiEndpoints.Conversations.ListAllConversations)]
    public async Task<IActionResult> ListAllConversations([FromQuery] Pagination pagination)
    {
        return Ok((await _conversationService.ListAllConversationsAsync(UserId, pagination)).ToPagedResponse(pagination));
    }

    [HttpGet(ApiEndpoints.Conversations.ListConversationMessages)]
    public async Task<IActionResult> ListConversationMessages(Guid conversationId, [FromQuery] Pagination pagination)
    {
        return Ok(await _messageService.ListConversationMessagesAsync(conversationId, pagination));
    }
}