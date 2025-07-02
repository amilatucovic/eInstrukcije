using RS1_2024_25.API.SignalR.DTOs;

namespace RS1_2024_25.API.SignalR.Interfaces
{
    public interface IMessageService
    {
        Task<List<MessageDto>> GetMessageHistoryAsync(int userId1, int userId2);
        Task<MessageDto> SendMessageAsync(int senderId, int receiverId, string content);
        Task<List<ConversationPreviewDto>> GetConversationListAsync(int currentUserId);
        Task<List<AvailableUsersDto>> GetAvailableUsersAsync(int currentUserId);

    }
}
