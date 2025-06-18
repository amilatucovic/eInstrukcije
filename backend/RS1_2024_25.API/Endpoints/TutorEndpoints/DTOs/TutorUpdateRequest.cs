namespace RS1_2024_25.API.Endpoints.TutorEndpoints.DTOs
{
    public class TutorUpdateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? CityId { get; set; }

        public string Qualifications { get; set; }
        public int YearsOfExperience { get; set; }
        public string Availability { get; set; }
        public string Policy { get; set; }
        public string HourlyRate { get; set; }
        public bool? IsLiveAvailable { get; set; }
    }
}
