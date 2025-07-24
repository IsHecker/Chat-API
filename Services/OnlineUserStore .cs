using System.Collections.Concurrent;

namespace Chat_API.Services;

public class OnlineUserStore
{
    private readonly ConcurrentDictionary<Guid, byte> _onlineUsers = new();

    public void AddUser(Guid userId)
    {
        _onlineUsers.TryAdd(userId, 0);
    }

    public void RemoveUser(Guid userId)
    {
        _onlineUsers.TryRemove(userId, out _);
    }

    public bool IsUserOnline(Guid userId)
    {
        return _onlineUsers.ContainsKey(userId);
    }

    public IEnumerable<Guid> GetOnlineUsers(IEnumerable<Guid> userIds)
    {
        return userIds.Where(_onlineUsers.ContainsKey);
    }

    public IEnumerable<Guid> GetAllOnlineUsers()
    {
        return _onlineUsers.Keys;
    }
}