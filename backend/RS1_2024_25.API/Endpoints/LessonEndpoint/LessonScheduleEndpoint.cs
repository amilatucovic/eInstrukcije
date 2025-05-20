using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.Entities;

[Route("api/[controller]")]
[ApiController]
public class LessonEndpoint(ApplicationDbContext db) : ControllerBase
{
    [HttpGet("schedule/{tutorId}")]
    public IActionResult GetScheduleForTutor(int tutorId)
    {
        var today = DateTime.Today;
        var diff = (int)today.DayOfWeek;
        var startOfWeek = today.AddDays(-diff).Date; // start from Sunday
        var endOfWeek = startOfWeek.AddDays(7);


        var lessons = db.Lessons
            .Include(l => l.Student)
            .Include(l => l.Subject)
            .Where(l => l.TutorID == tutorId &&
                        l.LessonDate >= startOfWeek &&
                        l.LessonDate < endOfWeek &&
                        l.Status == LessonStatus.Scheduled)
            .Select(l => new LessonScheduleDTO
            {
                LessonID = l.ID,
                SubjectName = l.Subject.Name,
                StudentName = l.Student.MyAppUser.FirstName + " " + l.Student.MyAppUser.LastName,
                Start = l.StartTime,
                End = l.EndTime,
                LessonMode = l.Mode.ToString(),
                Status = l.Status.ToString()
            })
            .ToList();

        return Ok(lessons);
    }
}
