using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RS1_2024_25.API.SignalR.Interfaces;
using System.Security.Claims;

namespace RS1_2024_25.API.SignalR
{
   
    public class ChatHub : Hub
    {

        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task SendMessage(string receiverId, string message)
        {
            var senderId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"SignalR: Pozvana SendMessage - senderId={senderId}, receiverId={receiverId}, content={message}");

            if (senderId == null) return;

            await _messageService.SendMessageAsync(int.Parse(senderId), int.Parse(receiverId), message);
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
