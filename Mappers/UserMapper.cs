using Chat_API.DTOs.Responses.Users;
using Chat_API.Models;

namespace Chat_API.Mappers;

public static class UserMapper
{
    public static GetProfileResponse ToProfileResponse(this User user)
    {
        return new GetProfileResponse
        {
            UserId = user.Id,
            Username = user.UserName!,
            Email = user.Email!,
            ProfilePictureUrl = user.ProfilePictureUrl,
            Status = user.IsOnline ? "Online" : "Offline",
        };
    }

    public static UserResponse ToResponse(this User user, string? role = null)
    {
        return new UserResponse
        {
            Id = user.Id,
            Username = user.UserName!,
            ProfilePictureUrl = user.ProfilePictureUrl,
            Role = role
        };
    }
}