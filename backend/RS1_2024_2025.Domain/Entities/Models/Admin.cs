
namespace RS1_2024_2025.Domain.Entities
{
    public class Admin : MyAppUser
    {
        public string Role {  get; set; }
        public bool CanApproveRequests { get; set; } = true;
        public bool CanViewLogs { get; set; } = true;


    }
}
