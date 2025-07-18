using Chat_API.DTOs.Requests.Auth;
using Chat_API.Mappers;
using Chat_API.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Chat_API.Controllers;

public class AuthController : ApiController
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost(ApiEndpoints.Auth.Signin)]
    public async Task<IActionResult> Signin(SigninRequest request)
    {
        var signInResult = await _authService.SignInAsync(request.ToCredentials());
        return signInResult.Match(Ok, Problem);
    }

    [HttpPost(ApiEndpoints.Auth.Register)]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var registerResult = await _authService.RegisterAsync(request.ToCredentials());
        return registerResult.Match(Created, Problem);
    }
}