using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.SignalR;

namespace Chat_API.Hubs.Configs
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User.FindFirst(JwtRegisteredClaimNames.Sub)!.Value;
        }
    }
}