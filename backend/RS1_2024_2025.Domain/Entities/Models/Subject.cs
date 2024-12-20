using System.ComponentModel.DataAnnotations;

namespace RS1_2024_2025.Domain.Entities
{
    public class Subject : IMyBaseEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DifficultyLevel { get; set; }

        public ICollection<TutorSubject> TutorSubjects { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
        public ICollection<SubjectCategory> SubjectCategories { get; set; }
    }
}
