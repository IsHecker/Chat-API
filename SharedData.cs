using Chat_API.Data;

namespace Chat_API;

public static class SharedData
{
    public static Guid UserId { get; set; }
    public static ApplicationDbContext Context { get; set; } = null!;
}