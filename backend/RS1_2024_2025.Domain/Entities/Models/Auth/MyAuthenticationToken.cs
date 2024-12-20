namespace RS1_2024_2025.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class MyAuthenticationToken
{
    [Key]
    public int ID { get; set; }

    public required string Value { get; set; } // Token string

    public string IpAddress { get; set; } = string.Empty;// IP address of the client

    public DateTime RecordedAt { get; set; } // Timestamp of token creation

    // Foreign key to link the token to a specific user
    [ForeignKey(nameof(MyAppUser))]
    public int MyAppUserId { get; set; }

    public MyAppUser? MyAppUser { get; set; } // Navigation property to the user
}
