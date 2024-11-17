using RS1_2024_25.API.Data.Models.Auth;

namespace RS1_2024_25.API.Data.Models
{
    public class Tutor : MyAppUser
    {
        public string Qualifications { get; set; }
        public int YearsOfExperience { get; set; }
        public string Availability {  get; set; }
        public double Rating { get; set; }
        public string Policy { get; set; }
        public string HourlyRate { get; set; }

      public ICollection<Review> Reviews { get; set; }
    }
}
