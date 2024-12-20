using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_2025.Domain.Entities.Models.Auth
{
    public class Message : IMyBaseEntity
    {
        public int ID { get; set; }

        [ForeignKey(nameof(MyAppUser))]
        public int SenderID { get; set; }
        public MyAppUser Sender { get; set; }

        [ForeignKey(nameof(MyAppUser))]
        public int ReceiverID { get; set; }
        public MyAppUser Receiver { get; set; }

        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }
}
