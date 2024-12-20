using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_2025.Domain.Entities
{
    public class StudentSubject 
    {
        [ForeignKey(nameof(Student))]
        public int StudentID { get; set; }
        public Student Student { get; set; }

        [ForeignKey(nameof(Subject))]
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }
    }
}
