using Chat_API.DTOs.Responses.Auth;
using Chat_API.Mappers;
using Chat_API.Models;
using Chat_API.Results;
using Chat_API.Services.Auth.Models;
using Microsoft.AspNetCore.Identity;

namespace Chat_API.Services.Auth;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly TokenGeneratorService _tokenGenerator;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        TokenGeneratorService tokenGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<Result<AuthResponse>> RegisterAsync(Credentials credentials)
    {
        var user = await _userManager.FindByEmailAsync(credentials.Email);

        if (user is not null)
            return Error.Conflict(description: "There's an Existing account with this Email");


        var userCreationResult = await CreateUserAsync(credentials);
        if (userCreationResult.IsError)
            return userCreationResult.Errors;

        return _tokenGenerator.GenerateToken(userCreationResult.Value, [credentials.Role ?? ""]).ToResponse();
    }

    public async Task<Result<AuthResponse>> SignInAsync(Credentials credentials)
    {
        var user = await _userManager.FindByEmailAsync(credentials.Email);

        if (user is null)
            return Error.Unauthorized(description: "Invalid email or password.");

        if (credentials.Password is not null)
        {
            var signInResult =
                await _signInManager.CheckPasswordSignInAsync(user, credentials.Password, lockoutOnFailure: false);

            if (!signInResult.Succeeded)
                return Error.Unauthorized(description: "Invalid email or password.");
        }

        return _tokenGenerator.GenerateToken(user, await _userManager.GetRolesAsync(user)).ToResponse();
    }

    private async Task<Result<User>> CreateUserAsync(Credentials credentials)
    {
        var newUser = new User
        {
            UserName = credentials.Username,
            Email = credentials.Email,
            IsOnline = true
        };

        IdentityResult userCreationResult =
            await _userManager.CreateAsync(newUser, credentials.Password);

        if (!userCreationResult.Succeeded)
            return Error.Validation(description: string.Join(", ", userCreationResult.Errors.Select(e => new { e.Code, e.Description })));

        if (!await _roleManager.RoleExistsAsync(credentials.Role!))
            return Error.Validation(description: $"Role '{credentials.Role}' does not exist.");

        var result = await _userManager.AddToRoleAsync(newUser, credentials.Role!);
        if (!result.Succeeded)
            return Error.Validation(description: string.Join(", ", result.Errors));

        return newUser;
    }
}