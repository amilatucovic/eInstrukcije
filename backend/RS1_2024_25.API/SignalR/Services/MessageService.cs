using Microsoft.EntityFrameworkCore;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.Entities.Models.Auth;
using RS1_2024_25.API.SignalR.DTOs;
using RS1_2024_25.API.SignalR.Interfaces;

namespace RS1_2024_25.API.SignalR.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;

        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MessageDto>> GetMessageHistoryAsync(int userId1, int userId2)
        {
            var messages = await _context.Messages
                .Include(m => m.Sender)
                .Where(m =>
                    (m.SenderID == userId1 && m.ReceiverID == userId2) ||
                    (m.SenderID == userId2 && m.ReceiverID == userId1))
                .OrderBy(m => m.SentAt)
                .ToListAsync();

            return messages.Select(m => new MessageDto
            {
                Id = m.ID,
                SenderId = m.SenderID,
                SenderUsername = m.Sender.Username,
                ReceiverId = m.ReceiverID,
                Content = m.Content,
                SentAt = m.SentAt,
                IsRead = m.IsRead
            }).ToList();
        }

        public async Task<MessageDto> SendMessageAsync(int senderId, int receiverId, string content)
        {
            var message = new Message
            {
                SenderID = senderId,
                ReceiverID = receiverId,
                Content = content,
                SentAt = DateTime.UtcNow,
                IsRead = false
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

           
            var savedMessage = await _context.Messages
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.ID == message.ID);

            return new MessageDto
            {
                Id = savedMessage.ID,
                SenderId = savedMessage.SenderID,
                SenderUsername = savedMessage.Sender.Username,
                SenderFullName = savedMessage.Sender.FirstName + " " + savedMessage.Sender.LastName,
                ReceiverId = savedMessage.ReceiverID,
                Content = savedMessage.Content,
                SentAt = savedMessage.SentAt,
                IsRead = savedMessage.IsRead
            };

        }

        public async Task<List<ConversationPreviewDto>> GetConversationListAsync(int currentUserId)
        {
            var messages = await _context.Messages
                .Where(m => m.SenderID == currentUserId || m.ReceiverID == currentUserId)
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .ToListAsync();

            var conversationDict = new Dictionary<int, Message>(); // key: otherUserId, value: latest message

            foreach (var message in messages)
            {
                var otherUserId = message.SenderID == currentUserId ? message.ReceiverID : message.SenderID;

                if (!conversationDict.ContainsKey(otherUserId) ||
                    message.SentAt > conversationDict[otherUserId].SentAt)
                {
                    conversationDict[otherUserId] = message;
                }
            }

            var previews = conversationDict.Select(kvp =>
            {
                var message = kvp.Value;
                var otherUser = message.SenderID == currentUserId ? message.Receiver : message.Sender;

                return new ConversationPreviewDto
                {
                    UserId = otherUser.ID,
                    FullName=otherUser.FirstName + " " + otherUser.LastName,
                    Username = otherUser.Username,
                    LastMessage = message.Content,
                    LastMessageTime = message.SentAt
                };
            })
            .OrderByDescending(p => p.LastMessageTime)
            .ToList();

            return previews;
        }



    }
}
