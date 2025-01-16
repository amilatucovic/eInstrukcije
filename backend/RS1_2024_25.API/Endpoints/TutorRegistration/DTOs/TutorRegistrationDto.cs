namespace RS1_2024_25.API.Endpoints.TutorRegistration.DTOs
{
    public class TutorRegistrationDto
    {

        //MyAppUser
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int CityId { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
      //  public byte[] ProfileImage { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        //Tutor
        public int YearsOfExperience { get; set; }
        public string HourlyRate { get; set; }
        public string Qualifications { get; set; }
        public string Availability { get; set; }
        public string Policy { get; set; }
        public bool? IsLiveAvailable { get; set; }
    }
}
