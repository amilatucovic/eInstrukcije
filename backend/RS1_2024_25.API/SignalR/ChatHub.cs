using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace RS1_2024_25.API.SignalR
{
   
    public class ChatHub : Hub
    {
        public async Task SendMessage(string receiverId, string message)
        {
            var senderId = Context.User?.FindFirst(ClaimTypes.Name)?.Value;
            if (senderId == null) return;

            // Pošalji poruku korisniku sa određenim ID-jem
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, message);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"User connected: {Context.UserIdentifier}");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"User disconnected: {Context.UserIdentifier}");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
