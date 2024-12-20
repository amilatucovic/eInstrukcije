namespace RS1_2024_2025.Domain.Entities;
using RS1_2024_2025.Domain.Entities.Models.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class MyAppUser : IMyBaseEntity
{
    [Key]
    public int ID { get; set; }
    public string Username { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public UserType UserType { get; set; }

    [ForeignKey(nameof(City))]
    public int? CityId { get; set; }
    public City? City { get; set; }

    public string? Gender { get; set; }

    public string RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public ICollection<Message> SentMessages { get; set; }
    public ICollection<Message> ReceivedMessages { get; set; }
    public string? ProfileImageUrl { get; set; }

}

public enum UserType { Admin, Tutor, Student }

//public enum Gender { Male, Female}
