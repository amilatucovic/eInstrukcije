using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace RS1_2024_25.API.SignalR
{
    public class NameUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        }
    }

}
