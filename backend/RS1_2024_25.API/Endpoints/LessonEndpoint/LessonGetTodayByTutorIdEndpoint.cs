using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_25.API.Endpoints.LessonEndpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonGetTodayByTutorIdEndpoint : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public LessonGetTodayByTutorIdEndpoint(ApplicationDbContext db)
        {
            this.db = db;
        }
 
        [HttpGet("tutor/{tutorId}")]
        public IActionResult GetTodayLessonsByTutor(int tutorId)
        {
            var today = DateTime.Now.Date;
            var tomorrow = today.AddDays(1);

            var lessons = db.Lessons
                                  .Include(l => l.Student)
                                  .ThenInclude(s => s.MyAppUser)
                                  .Where(l => l.TutorID == tutorId &&
                                   //l.LessonDate >= today &&
                                   //l.LessonDate < tomorrow &&
                                   l.Status == LessonStatus.Scheduled)
                        .Select(l => new LessonTodayResponse
                        {
                         LessonID = l.ID,
                         StudentName = l.Student.MyAppUser.FirstName + " " + l.Student.MyAppUser.LastName,
                         SubjectName = l.Subject.Name,
                         StartTime = l.StartTime.ToString("HH:mm"),
                         EndTime = l.EndTime.ToString("HH:mm"),
                         Mode = l.Mode.ToString()
                        })
                        .ToList();

            return Ok(lessons);
        }
    }
}

