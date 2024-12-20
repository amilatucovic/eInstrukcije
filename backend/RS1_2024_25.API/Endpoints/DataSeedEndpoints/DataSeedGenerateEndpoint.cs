namespace RS1_2024_2025.API.Endpoints.DataSeed;

using Microsoft.AspNetCore.Mvc;
using RS1_2024_2025.Domain;
using RS1_2024_2025.Domain.Entities;
using RS1_2024_2025.Domain.Entities.Models.Auth;
using RS1_2024_2025.API.Helper.Api;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RS1_2024_2025.Database;

[Route("data-seed")]
public class DataSeedGenerateEndpoint(ApplicationDbContext db)
    : MyEndpointBaseAsync
    .WithoutRequest
    .WithResult<string>
{
    [HttpPost]
    public override async Task<string> HandleAsync(CancellationToken cancellationToken = default)
    {
        if (db.MyAppUsers.Any())
        {
            throw new Exception("Podaci su vec generisani");
        }
        // Kreiranje država
       

        // Kreiranje gradova
        var cities = new List<City>
        {
            new City { Name = "Sarajevo" },
            new City { Name = "Mostar" },
            new City { Name = "Tuzla", },
            new City { Name = "Zenica", },
            new City { Name = "Bihac", },
            new City { Name = "Travnik"},
            new City { Name = "Bugojno" }
        };

        // Kreiranje korisnika s ulogama
        var users = new List<MyAppUser>
        {
            new MyAppUser
            {
                Username = "admin1",
                Password = "admin123",
                FirstName = "Admin",
                LastName = "One",
                
            },
            new MyAppUser
            {
                Username = "manager1",
                Password = "manager123",
                FirstName = "Manager",
                LastName = "One",
              
            },
            new MyAppUser
            {
                Username = "user1",
                Password = "user123",
                FirstName = "User",
                LastName = "One",
               
            },
            new MyAppUser
            {
                Username = "user2",
                Password = "user456",
                FirstName = "User",
                LastName = "Two",
               
            }
        };

        // Dodavanje podataka u bazu
       
        await db.Cities.AddRangeAsync(cities, cancellationToken);
        await db.MyAppUsers.AddRangeAsync(users, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        return "Data generation completed successfully.";
    }
}
