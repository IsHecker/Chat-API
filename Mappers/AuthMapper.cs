using Chat_API.DTOs.Requests.Auth;
using Chat_API.DTOs.Responses.Auth;
using Chat_API.Services.Auth.Models;

namespace Chat_API.Mappers;

public static class AuthMapper
{
    public static Credentials ToCredentials(this SigninRequest request)
    {
        return new Credentials
        {
            Email = request.Email,
            Password = request.Password
        };
    }

    public static Credentials ToCredentials(this RegisterRequest request)
    {
        return new Credentials
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password,
            Role = request.Role
        };
    }


    public static AuthResponse ToResponse(this JwtToken token)
    {
        return new AuthResponse
        {
            Token = token.Token,
            ExpiresIn = token.ExpiresIn,
            RefreshToken = token.RefreshToken
        };
    }
}