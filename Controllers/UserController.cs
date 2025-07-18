using Chat_API.DTOs.Requests.Users;
using Chat_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat_API.Controllers;

public class UserController : ApiController
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }


    [Authorize]
    [HttpGet(ApiEndpoints.Users.GetProfile)]
    public async Task<IActionResult> GetUserProfile()
    {
        var getProfileResult = await _userService.GetProfileAsync(UserId);
        return getProfileResult.Match(Ok, Problem);
    }

    [Authorize]
    [HttpPut(ApiEndpoints.Users.UpdateProfile)]
    public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request)
    {
        var getProfileResult = await _userService.UpdateProfileAsync(UserId, request);
        return getProfileResult.Match(NoContent, Problem);
    }
}