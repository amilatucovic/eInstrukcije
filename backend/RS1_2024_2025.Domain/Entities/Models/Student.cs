using RS1_2024_2025.Domain.Entities.Models.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RS1_2024_2025.Domain.Entities
{
    public enum EducationLevel
    {
        ElementarySchool,
        HighSchool
    }
    public class Student
    {
        [Key]
        public int ID { get; set; }
        public string Grade { get; set; }
        public string PreferredMode { get; set; }
        public EducationLevel EducationLevel { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<ReservationPayment> ReservationsPayment { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Lesson> Lessons { get; set; }


		[ForeignKey(nameof(MyAppUser))]
        public int MyAppUserID { get; set; }
        public MyAppUser MyAppUser { get; set; }
    }
}
