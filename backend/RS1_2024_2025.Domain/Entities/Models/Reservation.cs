using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_2025.Domain.Entities
{
    public enum ReservationStatus
    {
        Pending,
        Approved,
        Paid,
        Rejected
    }

    public class Reservation : IMyBaseEntity
    {
        public int ID { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentID { get; set; }
        public Student Student { get; set; }

        [ForeignKey(nameof(Tutor))]
        public int TutorID { get; set; }
        public Tutor Tutor { get; set; }

        [ForeignKey(nameof(Lesson))]
        public int LessonID { get; set; }
        public Lesson Lesson { get; set; }

        public ReservationStatus Status { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? RejectedDate { get; set; }
        public ICollection<ReservationPayment> ReservationPayments { get; set; }

    }

    // Ako dodje do pomjeranja, validacija da se ne moze za tog tutora pomjeriti na datum kada tutor vec ima zakazan cas
    // Npr. imas rezervaciju na dan 19.12.2024 u 5 sa denisom
    // Denis pomjera termin, samo validacija da ne pomjeri na termin koji mu je vec zauzet

    // Znaci provjeriti reservations where status == approved && datum == ovom na koji zelim pomjeriti, ako vec postoji takva rezervacija
    // ne uraditi nista samo vratiti poruku vec imate termin na taj datum i vrijeme

    // A ako prodje request za pomjeranjem, onda ce se postojeca rezervacija azurirat samo ce reservation date bit taj novi
}
