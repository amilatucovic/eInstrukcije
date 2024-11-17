using RS1_2024_25.API.Helper;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    public class Review  : IMyBaseEntity
    {
        public int ID { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentID { get; set; }  
        public Student Student { get; set; }

        [ForeignKey(nameof(Tutor))]
        public int TutorID { get; set; }
        public Tutor Tutor { get; set; }

        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }




    }
}
