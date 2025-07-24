using Chat_API.DTOs.Requests.Common;
using Chat_API.Models;
using Chat_API.Models.Joins;
using Chat_API.Services;
using Microsoft.EntityFrameworkCore;

namespace Chat_API.Data.Repositories;

public class GroupConversationRepository : EntityRepository<GroupConversation, GroupConversationRepository>
{
    private readonly OnlineUserStore _onlineUserStore;
    public GroupConversationRepository(ApplicationDbContext context, OnlineUserStore onlineUserStore) : base(context)
    {
        _onlineUserStore = onlineUserStore;
    }

    public async Task<IEnumerable<GroupConversation>> GetAllByUserIdAsync(Guid userId, Pagination pagination)
    {
        return await Query.Where(m => m.Members.Any(u => u.Id == userId))
                        .OrderBy(ic => ic.LastActivityAt)
                        .Paginate(pagination)
                        .ToListAsync();
    }

    public async Task<IEnumerable<User>?> ListGroupMembersAsync(Guid conversationId, Pagination pagination)
    {
        return (await Query.Include(grp => grp.Members)
            .Paginate(pagination)
            .FirstOrDefaultAsync(gc => gc.Id == conversationId))?.Members;
    }

    public async Task<IEnumerable<Guid>> ListGroupMemberIdsAsync(Guid conversationId)
    {
        return await Context.GroupMembers
            .Where(gm => gm.GroupConversationId == conversationId && _onlineUserStore.IsUserOnline(gm.MembersId))
            .Select(gm => gm.MembersId)
            .ToListAsync();
    }

    public Task<IEnumerable<Guid>> ListGroupAdminIdsAsync(Guid conversationId)
    {
        return Task.FromResult<IEnumerable<Guid>>(
            Context.GroupAdmins
                .Where(ga => ga.GroupConversationId == conversationId)
                .Select(ga => ga.AdminsId));
    }

    public async Task AddMemberAsync(Guid conversationId, Guid memberId)
    {
        await Context.GroupMembers.AddAsync(new GroupMember
        {
            MembersId = memberId,
            GroupConversationId = conversationId
        });
    }

    public async Task AddMemberAsync(Guid conversationId, IEnumerable<Guid> memberIds)
    {
        foreach (var memberId in memberIds)
        {
            await AddMemberAsync(conversationId, memberId);
        }
    }

    public async Task AddAdminAsync(Guid conversationId, Guid adminId)
    {
        await Context.GroupAdmins.AddAsync(new GroupAdmin
        {
            AdminsId = adminId,
            GroupConversationId = conversationId
        });
    }

    public async Task AddAdminAsync(Guid conversationId, IEnumerable<Guid> adminIds)
    {
        foreach (var adminId in adminIds)
        {
            await AddAdminAsync(conversationId, adminId);
        }
    }

    public async Task RemoveMemberAsync(Guid conversationId, Guid memberId)
    {
        await Context.GroupMembers
            .Where(gm => gm.GroupConversationId == conversationId && gm.MembersId == memberId)
            .ExecuteDeleteAsync();
    }

    public async Task<bool> IsMemberExistAsync(Guid conversationId, Guid memberId)
    {
        return await Context.GroupMembers
            .Where(gm => gm.GroupConversationId == conversationId)
            .AnyAsync(gm => gm.MembersId == memberId);
    }

    public async Task<bool> IsMemberExistAsync(Guid conversationId, IEnumerable<Guid> memberIds)
    {
        return await Context.GroupMembers
            .Where(gm => gm.GroupConversationId == conversationId)
            .AnyAsync(gm => memberIds.Contains(gm.MembersId));
    }

    public async Task<bool> IsAdminAsync(Guid conversationId, Guid adminId)
    {
        return await Context.GroupAdmins
            .Where(gm => gm.GroupConversationId == conversationId)
            .AnyAsync(gm => gm.AdminsId == adminId);
    }

    public async Task UpdateDetails(Guid conversationId, string? name = null, string? groupPictureUrl = null)
    {
        await Query.Where(gc => gc.Id == conversationId).ExecuteUpdateAsync(setters =>
            setters
                .SetProperty(a => a.Name, a => name ?? a.Name)
                .SetProperty(a => a.GroupPictureUrl,
                     a => groupPictureUrl ?? a.GroupPictureUrl)
        );
    }

    public async Task UpdateLastActivityAsync(Guid conversationId)
    {
        await Query.Where(gc => gc.Id == conversationId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(p => p.LastActivityAt, DateTime.UtcNow));
    }
}