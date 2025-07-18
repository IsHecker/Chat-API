using Chat_API.Models;

namespace Chat_API.Data.Repositories;

public class FriendRequestRepository : EntityRepository<FriendRequest, FriendRequestRepository>
{
    public FriendRequestRepository(ApplicationDbContext context) : base(context)
    {
    }
}