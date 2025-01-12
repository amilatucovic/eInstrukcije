using RS1_2024_2025.Domain.Entities.Models.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_2025.Domain.Entities
{
    public class Tutor
    {

        [Key]
        public int ID { get; set; }
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

        [ForeignKey(nameof(MyAppUser))]
        public int MyAppUserID { get; set; }
        public MyAppUser MyAppUser { get; set; }
    }
}
