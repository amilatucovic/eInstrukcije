

namespace RS1_2024_2025.Domain.Entities
{
    //Ova tabela čuva kategorije predmeta (za jezike: Gramatika, Eseji, Pravopis)
    public class Category : IMyBaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<SubjectCategory> SubjectCategories { get; set; }
    }
}
