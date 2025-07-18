using Chat_API.Models;

namespace Chat_API.Data.Repositories;

public class FriendshipRepository : Repository<Friendship, FriendshipRepository>
{
    public FriendshipRepository(ApplicationDbContext context) : base(context)
    {
    }
}