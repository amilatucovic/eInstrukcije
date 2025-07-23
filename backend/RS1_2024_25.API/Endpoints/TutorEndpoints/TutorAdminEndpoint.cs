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
            if (tutor == null)
                return NotFound();

          
            if (request.Qualifications != null)
                tutor.Qualifications = request.Qualifications;

            if (request.YearsOfExperience.HasValue)
                tutor.YearsOfExperience = request.YearsOfExperience.Value;

            if (request.Availability != null)
                tutor.Availability = request.Availability;

            if (request.Policy != null)
                tutor.Policy = request.Policy;

            if (request.HourlyRate != null)
                tutor.HourlyRate = request.HourlyRate;

            if (request.IsLiveAvailable.HasValue)
                tutor.IsLiveAvailable = request.IsLiveAvailable.Value;

           
            if (request.FirstName != null)
                tutor.MyAppUser.FirstName = request.FirstName;

            if (request.LastName != null)
                tutor.MyAppUser.LastName = request.LastName;

            if (request.Email != null)
                tutor.MyAppUser.Email = request.Email;

            if (request.PhoneNumber != null)
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
            var tutor = await db.Tutors
                .Include(t => t.MyAppUser)
                .Include(t => t.TutorSubjects)
                .Include(t => t.Reviews)
                .Include(t => t.ReservationsPayment)
                .FirstOrDefaultAsync(t => t.ID == id);

            if (tutor == null)
                return NotFound();

            var userId = tutor.MyAppUserID;

            var messages = await db.Messages
                .Where(m => m.SenderID == userId || m.ReceiverID == userId)
                .ToListAsync();
            db.Messages.RemoveRange(messages);

            var categories = await db.TutorSubjectCategories
    .Where(c => c.TutorID == tutor.ID)
    .ToListAsync();
            db.TutorSubjectCategories.RemoveRange(categories);


            var lessons = await db.Lessons
                .Where(l => l.TutorID == tutor.ID)
                .Include(l => l.ReservationsPayment)
                .ToListAsync();

            foreach (var lesson in lessons)
                db.ReservationsPayments.RemoveRange(lesson.ReservationsPayment);

            var lessonIds = lessons.Select(l => l.ID).ToList();
            var reservations = await db.Reservations
                .Where(r => lessonIds.Contains(r.LessonID))
                .ToListAsync();
            db.Reservations.RemoveRange(reservations);

            db.Lessons.RemoveRange(lessons);

            
            db.TutorsSubjects.RemoveRange(tutor.TutorSubjects);
            db.Reviews.RemoveRange(tutor.Reviews);
            db.ReservationsPayments.RemoveRange(tutor.ReservationsPayment);

            
            db.Tutors.Remove(tutor);
            db.MyAppUsers.Remove(tutor.MyAppUser);

            try
            {
                await db.SaveChangesAsync();
                return Ok(new { message = "Tutor deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
        }







    }
}


    

