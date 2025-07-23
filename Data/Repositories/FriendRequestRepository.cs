using Chat_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat_API.Data.Repositories;

public class FriendRequestRepository : EntityRepository<FriendRequest, FriendRequestRepository>
{
    public FriendRequestRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> RequestExistsAsync(Guid senderId, Guid receiverId)
    {
        return await Query.AnyAsync(fr => fr.SenderId == senderId && fr.ReceiverId == receiverId);
    }

    public async Task<Guid?> GetFriendRequestId(Guid senderId, Guid receiverId)
    {
        return await Query
            .Where(fr => fr.SenderId == senderId && fr.ReceiverId == receiverId)
            .Select(fr => fr.Id)
            .FirstOrDefaultAsync();
    }
}