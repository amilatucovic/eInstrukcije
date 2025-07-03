using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RS1_2024_25.API.SignalR.Interfaces;
using System.Security.Claims;

namespace RS1_2024_25.API.SignalR
{
   
    public class ChatHub : Hub
    {

        private readonly IMessageService _messageService;

        private int GetCurrentUserId()
        {
            var userIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                throw new Exception("User ID claim not found in context.");

            return int.Parse(userIdClaim);
        }


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

        public async Task Typing(int receiverId)
        {
            var senderId = GetCurrentUserId(); 
            await Clients.User(receiverId.ToString()).SendAsync("UserTyping", senderId);
        }
    }
}
