using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_2025.Domain.Entities;

public class City: IMyBaseEntity
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; }
    public string PostalCode { get; set; }
    

   
}