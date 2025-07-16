using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS1_2024_2025.Domain.Entities.Models
{
    public class TutorSubjectCategory
    {
        public int ID { get; set; }  

        [ForeignKey(nameof(Tutor))]
        public int TutorID { get; set; }
        public Tutor Tutor { get; set; }

        [ForeignKey(nameof(Subject))]
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }

        [ForeignKey(nameof(Category))]
        public int? CategoryID { get; set; }
        public Category? Category { get; set; }
    }


}
