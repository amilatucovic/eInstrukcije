using RS1_2024_25.API.Data.Models.Auth;
using RS1_2024_25.API.Helper;

namespace RS1_2024_25.API.Data.Models
{
    public class Student : MyAppUser
    {
        
        public string Grade { get; set; }
        public string PreferredMode { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
