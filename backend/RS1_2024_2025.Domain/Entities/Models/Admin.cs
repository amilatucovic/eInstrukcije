using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RS1_2024_2025.Domain.Entities
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }
        public string Role {  get; set; }
        public bool CanApproveRequests { get; set; } = true;
        public bool CanViewLogs { get; set; } = true;
        
        [ForeignKey(nameof(MyAppUser))]
        public int MyAppUserID { get; set; }
        public MyAppUser MyAppUser { get; set; }
    }
}
