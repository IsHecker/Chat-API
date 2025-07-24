using Chat_API.DTOs.Requests.Common;
using Chat_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat_API.Data.Repositories;

public class IndividualConversationRepository : EntityRepository<IndividualConversation, IndividualConversationRepository>
{
    public IndividualConversationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IndividualConversation?> GetByUserIdAsync(Guid userId)
    {
        return await Query.FirstOrDefaultAsync(conv => conv.User1Id == userId || conv.User2Id == userId);
    }

    public Task<IEnumerable<IndividualConversation>> GetAllByUserIdAsync(Guid userId, Pagination pagination)
    {
        return Task.FromResult<IEnumerable<IndividualConversation>>(
            Query.Where(conv => conv.User1Id == userId || conv.User2Id == userId)
                .OrderBy(ic => ic.LastActivityAt)
                .Paginate(pagination));
    }
    public async Task UpdateLastActivityAsync(Guid conversationId)
    {
        await Query.Where(gc => gc.Id == conversationId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(p => p.LastActivityAt, DateTime.UtcNow));
    }
}