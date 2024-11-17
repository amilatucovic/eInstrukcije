using RS1_2024_25.API.Data.Models.Auth;

namespace RS1_2024_25.API.Data.Models
{
    public class Admin : MyAppUser
    {
        public string Role {  get; set; }
        public bool CanApproveRequests { get; set; } = true;
        public bool CanViewLogs { get; set; } = true;


    }
}
