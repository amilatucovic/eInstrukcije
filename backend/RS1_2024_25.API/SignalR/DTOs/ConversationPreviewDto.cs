namespace RS1_2024_25.API.SignalR.DTOs
{
    public class ConversationPreviewDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string LastMessage { get; set; }
        public DateTime? LastMessageTime { get; set; }
    }
}
