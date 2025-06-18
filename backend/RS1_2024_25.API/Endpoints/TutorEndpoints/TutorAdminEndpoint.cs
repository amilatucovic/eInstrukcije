using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.SearchObjects;
using RS1_2024_25.API.Endpoints.TutorEndpoints.DTOs;

namespace RS1_2024_25.API.Endpoints.TutorEndpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorAdminEndpoint(ApplicationDbContext db) : ControllerBase
    {
        [HttpGet("get-all")]
        public IActionResult GetAllTutors([FromQuery] AdminTutorSearchObject? search)
        {
            var query = db.Tutors
                .Include(t => t.MyAppUser)
                .Include(t => t.MyAppUser.City)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.FirstName))
            {
                query = query.Where(t => t.MyAppUser.FirstName.StartsWith(search.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(search?.LastName))
            {
                query = query.Where(t => t.MyAppUser.LastName.StartsWith(search.LastName));
            }

            if (search?.CityId != null)
            {
                query = query.Where(t => t.MyAppUser.CityId == search.CityId);
            }

            if (search?.IsLiveAvailable != null)
            {
                query = query.Where(t => t.IsLiveAvailable == search.IsLiveAvailable);
            }         
            var tutors = query.ToList();
            if (!string.IsNullOrWhiteSpace(search?.MaxHourlyRate) && double.TryParse(search.MaxHourlyRate, out var maxRate))
            {
                tutors = tutors.Where(t =>
                {
                    var numericPart = new string(t.HourlyRate.TakeWhile(char.IsDigit).ToArray());
                    return double.TryParse(numericPart, out var parsedRate) && parsedRate <= maxRate;
                }).ToList();
            }



            var result = tutors.Select(t => new
            {
                t.ID,
                t.Qualifications,
                t.YearsOfExperience,
                t.Availability,
                t.Policy,
                t.HourlyRate,
                t.IsLiveAvailable,
                FirstName = t.MyAppUser.FirstName,
                LastName = t.MyAppUser.LastName,
                Email = t.MyAppUser.Email,
                PhoneNumber = t.MyAppUser.PhoneNumber,
                CityName = t.MyAppUser.City?.Name ?? "Nepoznat"
            }).ToList();

            return Ok(result);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTutor(int id, [FromBody] TutorUpdateRequest request)
        {
            var tutor = db.Tutors.Include(t => t.MyAppUser).FirstOrDefault(t => t.ID == id);
            if (tutor == null) return NotFound();

            
            tutor.Qualifications = request.Qualifications;
            tutor.YearsOfExperience = request.YearsOfExperience;
            tutor.Availability = request.Availability;
            tutor.Policy = request.Policy;
            tutor.HourlyRate = request.HourlyRate;
            tutor.IsLiveAvailable = request.IsLiveAvailable;

            
            tutor.MyAppUser.FirstName = request.FirstName;
            tutor.MyAppUser.LastName = request.LastName;
            tutor.MyAppUser.Email = request.Email;
            tutor.MyAppUser.PhoneNumber = request.PhoneNumber;
            if (request.CityId.HasValue)
            {
                var cityExists = db.Cities.Any(c => c.ID == request.CityId.Value);
                if (!cityExists)
                    return BadRequest("Grad s datim ID-jem ne postoji.");

                tutor.MyAppUser.CityId = request.CityId.Value;
            }


            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTutor(int id)
        {
            var tutor = db.Tutors.Include(t => t.MyAppUser).FirstOrDefault(t => t.ID == id);
            if (tutor == null)
                return NotFound();

            db.MyAppUsers.Remove(tutor.MyAppUser);
            db.Tutors.Remove(tutor);
            await db.SaveChangesAsync();

            return Ok("Tutor deleted successfully");
        }
    }
}


    

