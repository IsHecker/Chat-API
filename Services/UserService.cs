using Chat_API.DTOs.Requests.Users;
using Chat_API.DTOs.Responses.Users;
using Chat_API.Mappers;
using Chat_API.Models;
using Chat_API.Results;
using Microsoft.AspNetCore.Identity;

namespace Chat_API.Services;

public class UserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<GetProfileResponse>> GetProfileAsync(Guid userId)
    {
        var userProfile = await _userManager.FindByIdAsync(userId.ToString());
        if (userProfile is null)
            return Error.NotFound(description: "This Profile is not found.");

        return userProfile.ToResponse();
    }

    public async Task<Result> UpdateProfileAsync(Guid userId, UpdateProfileRequest updatedProfile)
    {
        var userProfile = await _userManager.FindByIdAsync(userId.ToString());
        if (userProfile is null)
            return Error.NotFound(description: "This Profile is not found.");

        userProfile.UpdateProfile(updatedProfile.Username, updatedProfile.ProfilePictureUrl);
        var updateResult = await _userManager.UpdateAsync(userProfile);
        if (!updateResult.Succeeded)
            return Error.Validation(description: string.Join(", ", updateResult.Errors.Select(e => new { e.Code, e.Description })));

        return Result.Success;
    }
}