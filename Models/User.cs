using Microsoft.AspNetCore.Identity;

namespace Chat_API.Models;

public class User : IdentityUser<Guid>
{
    public string? ProfilePictureUrl { get; private set; } = null!;
    public bool IsOnline { get; init; }

    public void UpdateProfile(string? newUsername, string? newPicture)
    {
        UserName = newUsername;
        ProfilePictureUrl = newPicture;
    }
}