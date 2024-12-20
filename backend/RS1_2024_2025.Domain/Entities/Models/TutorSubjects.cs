using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_2025.Domain.Entities
{
    public class TutorSubject 
    {

        [ForeignKey(nameof(Tutor))]
        public int TutorID { get; set; }
        public Tutor Tutor { get; set; }

        [ForeignKey(nameof (Subject))]
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }

    }
}
