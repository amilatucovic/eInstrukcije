using RS1_2024_25.API.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models;

public class City: IMyBaseEntity
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; }
    public string PostalCode { get; set; }
    

   
}