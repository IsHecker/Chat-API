using Chat_API.DTOs.Requests.Common;
using Chat_API.DTOs.Requests.Groups;
using Chat_API.Filters;
using Chat_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat_API.Controllers;

[Authorize]
public class GroupController : ApiController
{
    private readonly GroupService _groupService;

    public GroupController(GroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpGet(ApiEndpoints.Groups.GetGroupDetails)]
    public async Task<IActionResult> GetGroupDetails(Guid conversationId)
    {
        var getDetailsResult = await _groupService.GetGroupDetailsAsync(conversationId);
        return getDetailsResult.Match(Ok, Problem);
    }

    [HttpGet(ApiEndpoints.Groups.ListGroupMembers)]
    public async Task<IActionResult> ListGroupMembers(Guid conversationId, [FromQuery] Pagination pagination)
    {
        var listResult = await _groupService.ListGroupMembersAsync(conversationId, pagination);
        return listResult.Match(Ok, Problem);
    }

    [HttpPost(ApiEndpoints.Groups.AddMembersToGroup)]
    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    public async Task<IActionResult> AddMembersToGroup(Guid conversationId, AddMembersToGroupRequest request)
    {
        var addMembersResult = await _groupService.AddMembersAsync(conversationId, request.MemberIds);
        return addMembersResult.Match(NoContent, Problem);
    }

    [HttpDelete(ApiEndpoints.Groups.RemoveMemberFromGroup)]
    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    public async Task<IActionResult> RemoveMemberFromGroup(Guid conversationId, Guid memberId)
    {
        var removeMembersResult = await _groupService.RemoveMemberAsync(conversationId, memberId);
        return removeMembersResult.Match(NoContent, Problem);
    }

    [HttpPost(ApiEndpoints.Groups.Create)]
    public async Task<IActionResult> CreateGroup(CreateGroupRequest request)
    {
        var createGroupResult = await _groupService.CreateNewGroupAsync(UserId, request);
        return createGroupResult.Match(Created, Problem);
    }

    [HttpPut(ApiEndpoints.Groups.UpdateGroupDetails)]
    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    public async Task<IActionResult> UpdateGroupDetails(Guid conversationId, UpdateGroupDetailsRequest request)
    {
        var createGroupResult = await _groupService.UpdateGroupDetails(conversationId, request);
        return createGroupResult.Match(NoContent, Problem);
    }
}