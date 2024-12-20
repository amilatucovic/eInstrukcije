using RS1_2024_2025.Domain.Entities.Models.Auth;

namespace RS1_2024_2025.Domain.Entities
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
        public ICollection<ReservationPayment> ReservationsPayment { get; set; }    
        public ICollection<TutorSubject> TutorSubjects { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
