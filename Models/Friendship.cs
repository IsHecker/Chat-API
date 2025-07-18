namespace Chat_API.Models;

public class Friendship
{
    public Guid User1Id { get; init; }
    public Guid User2Id { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}