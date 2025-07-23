using System.IdentityModel.Tokens.Jwt;
using Chat_API.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chat_API.Filters;

public class AdminAuthorizationFilter : IAsyncAuthorizationFilter
{
    private readonly GroupConversationRepository _groupRepository;

    public AdminAuthorizationFilter(GroupConversationRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var userIdClaim = context.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
        {
            // User is not authenticated or user ID is invalid
            context.Result = new ForbidResult(); // Or UnauthorizedResult if no user is logged in at all
            return;
        }

        // Get the Group ID from the route data
        if (!context.RouteData.Values.TryGetValue("conversationId", out var conversationIdObj) ||
            !Guid.TryParse(conversationIdObj?.ToString(), out Guid conversationId))
        {
            context.Result = new BadRequestObjectResult("Group ID is missing or invalid in the request.");
            return;
        }

        if (!await _groupRepository.IsAdminAsync(conversationId, userId))
        {
            context.Result = new ObjectResult("Only group Admins can perform this action in this group.")
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }
    }
}