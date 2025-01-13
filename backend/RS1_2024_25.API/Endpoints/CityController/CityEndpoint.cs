using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_25.API.Endpoints.CityEndpoint
{
    [Route("api/[controller]")]
    [ApiController]

    public class CityEndpoint(ApplicationDbContext db) : ControllerBase
    {
        
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var cities = await db.Cities
               .Select(c => new City
               {
                   ID = c.ID,
                   Name = c.Name,
                   PostalCode = c.PostalCode,

               })
               .ToListAsync();
            return Ok(cities);
        }

        [HttpGet("byName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var cities = await db.Cities
                                 .Where(c => c.Name.Contains(name))
                                 .ToListAsync();

            if (cities == null || cities.Count == 0)
            {
                return NotFound($"No cities found with name containing '{name}'.");
            }

            return Ok(cities);
        }

        [HttpGet("byPostalCode/{postalCode}")]
        public async Task<IActionResult> GetByPostalCode(string postalCode)
        {
            var cities = await db.Cities
                                 .Where(c => c.PostalCode == postalCode)
                                 .ToListAsync();

            if (cities == null || cities.Count == 0)
            {
                return NotFound($"No cities found with postal code '{postalCode}'.");
            }

            return Ok(cities);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var city = await db.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound($"City with ID {id} not found.");
            }

            db.Cities.Remove(city);
            await db.SaveChangesAsync();

            return Ok();
        }




    }
}
