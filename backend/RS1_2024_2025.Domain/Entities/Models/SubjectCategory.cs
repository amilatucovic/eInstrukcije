using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_2025.Domain.Entities
{
    public class SubjectCategory 
    {
       
        [ForeignKey(nameof(Subject))]
        public int SubjectID { get; set; }  
        public Subject Subject { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryID { get; set; }
        public Category Category { get; set; }
      
    }
}
