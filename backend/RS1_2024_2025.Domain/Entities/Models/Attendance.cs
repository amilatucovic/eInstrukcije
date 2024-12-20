using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_2025.Domain.Entities
{
    public class Attendance
    {
        [ForeignKey(nameof(Lesson))]
        public int LessonID { get; set; }
        public Lesson Lesson { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentID { get; set; }
        public Student Student { get; set; }

        public bool IsPresent { get; set; }
    }
}
