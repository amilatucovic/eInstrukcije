
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_2025.Domain.Entities
{

    public enum LessonStatus
    {
        Held,     
        Missed,
        Cancelled,
        Postponed
    }

    public enum LessonMode
    {
        InPerson, 
        Online    
    }
    public class Lesson : IMyBaseEntity
    {
        public int ID { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentID { get; set; }
        public Student Student { get; set; }
       

        [ForeignKey(nameof(Tutor))]
        public int TutorID { get; set; }    
        public Tutor Tutor { get; set; }

        [ForeignKey(nameof(Subject))]
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }

        public DateTime LessonDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Reservation Reservation { get; set; }  //navigacijski property

        public int Duration
        {
            get
            {
                return (EndTime - StartTime).Minutes;
            }
            set
            {
               
                Duration = value;
            }
        }
        public LessonStatus Status { get; set; }  
        public LessonMode Mode { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<ReservationPayment> ReservationsPayment { get; set; }

    }
}
