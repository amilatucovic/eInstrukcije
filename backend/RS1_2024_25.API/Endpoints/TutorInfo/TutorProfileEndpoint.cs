using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_25.API.Endpoints.TutorInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorProfileEndpoint : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public TutorProfileEndpoint(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet("{tutorId}")]
        public IActionResult GetTutorProfile(int tutorId)
        {
            var tutor = db.Tutors
                .Include(t => t.MyAppUser)
                .FirstOrDefault(t => t.ID == tutorId);

            if (tutor == null || tutor.MyAppUser == null)
                return NotFound("Tutor not found");

            var response = new
            {
                FirstName = tutor.MyAppUser.FirstName,
                LastName = tutor.MyAppUser.LastName,
                ProfileImageUrl = tutor.MyAppUser.ProfileImageUrl,
                Rating = tutor.Rating,
                Qualifications = tutor.Qualifications,
                Experience = tutor.YearsOfExperience
               
            };

            return Ok(response);
        }
    }
}
