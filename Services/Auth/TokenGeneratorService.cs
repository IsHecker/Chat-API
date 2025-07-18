using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chat_API.Models;
using Chat_API.Services.Auth.Models;
using Microsoft.IdentityModel.Tokens;

namespace Chat_API.Services.Auth;

public class TokenGeneratorService
{
    private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);

    private readonly IConfiguration _configuration;

    public TokenGeneratorService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public JwtToken GenerateToken(User user, IEnumerable<string>? roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!);
        var expiresIn = DateTimeOffset.UtcNow.Add(TokenLifetime);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // your internal user ID
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new("name", user.UserName!),
            new(ClaimTypes.Role, $"[{string.Join(',', roles??[])}]"),
            new("auth_provider", "Google"),
            new("picture", user.ProfilePictureUrl ?? ""),
            new(JwtRegisteredClaimNames.Iat,
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiresIn.DateTime,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new JwtToken
        {
            Token = tokenHandler.WriteToken(token),
            ExpiresIn = expiresIn.ToUnixTimeMilliseconds(),
            RefreshToken = ""
        };
    }
}