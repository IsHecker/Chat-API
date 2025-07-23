using Chat_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat_API.Data.Repositories;

public class FriendshipRepository : Repository<Friendship, FriendshipRepository>
{
    public FriendshipRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> IsFriendsAsync(Guid userId, Guid friendId)
    {
        return await Query.AnyAsync(fs =>
            (fs.User1Id == userId && fs.User2Id == friendId)
            || (fs.User1Id == friendId && fs.User2Id == userId));
    }
}