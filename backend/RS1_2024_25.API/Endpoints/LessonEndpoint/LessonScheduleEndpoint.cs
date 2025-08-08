using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.Entities;
using RS1_2024_25.API.Endpoints.LessonEndpoint;

[Route("api/[controller]")]
[ApiController]
public class LessonEndpoint(ApplicationDbContext db) : ControllerBase
{
	[HttpGet("all")]
	public IActionResult GetAllLessons()
	{
		var lessons = db.Lessons
			.Include(l => l.Student)
				.ThenInclude(s => s.MyAppUser)
			.Include(l => l.Tutor)
				.ThenInclude(t => t.MyAppUser)
			.Include(l => l.Subject)
			.Select(l => new LessonScheduleDTO
			{
				LessonID = l.ID,
				StudentName = l.Student.MyAppUser.FirstName + " " + l.Student.MyAppUser.LastName,
				StudentId=l.StudentID,
				SubjectID=l.SubjectID,
				TutorName = l.Tutor.MyAppUser.FirstName + " " + l.Tutor.MyAppUser.LastName,
				SubjectName = l.Subject.Name,
				Start = l.StartTime,
				End = l.EndTime,
				LessonMode=l.Mode,
				Status = l.Status.ToString(),
				TutorId = l.TutorID
			})
			.ToList();

		return Ok(lessons);
	}

	[HttpGet("schedule/{tutorId}")]
	public IActionResult GetScheduleForTutor(int tutorId)
	{
		var lessons = db.Lessons
			.Include(l => l.Student)
				.ThenInclude(s => s.MyAppUser)
			.Include(l => l.Subject)
			.Include(l => l.Tutor)
				.ThenInclude(t => t.MyAppUser)
			.Where(l => l.TutorID == tutorId && l.Status == LessonStatus.Scheduled)
			.Select(l => new LessonScheduleDTO
			{
				LessonID = l.ID,
				SubjectName = l.Subject.Name,
				StudentName = l.Student.MyAppUser.FirstName + " " + l.Student.MyAppUser.LastName,
				StudentId = l.StudentID,
				SubjectID = l.SubjectID,
				Start = l.StartTime,
				End = l.EndTime,
				LessonMode = l.Mode,
				Status = l.Status.ToString(),
				TutorName = l.Tutor.MyAppUser.FirstName + " " + l.Tutor.MyAppUser.LastName,
				TutorId = l.TutorID,
			})
			.ToList();

		if (!lessons.Any())
			return Ok(new List<LessonScheduleDTO>());

		return Ok(lessons);
	}


	[HttpGet("lessons/{studentId}")]
	public IActionResult GetScheduleForStudent(int studentId)
	{
		var today = DateTime.Today;

		int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
		var startOfWeek = today.AddDays(-diff).Date;
		var endOfWeek = startOfWeek.AddDays(7);

		var lessons = db.Lessons
			.Include(l => l.Tutor)
				.ThenInclude(t => t.MyAppUser)
			.Include(l => l.Subject)
			.Where(l => l.StudentID == studentId &&
						l.LessonDate >= startOfWeek &&
						l.LessonDate < endOfWeek &&
						l.Status == LessonStatus.Scheduled)
			.Select(l => new LessonScheduleDTO
			{
				LessonID = l.ID,
				StudentId = l.Student.ID,
				StudentName = l.Student.MyAppUser.FirstName + " " + l.Student.MyAppUser.LastName,
				SubjectID=l.SubjectID,
				SubjectName = l.Subject.Name,
				Start = l.StartTime,
				End = l.EndTime,
				LessonMode = l.Mode,
				Status = l.Status.ToString(),
				TutorName = l.Tutor.MyAppUser.FirstName + " " + l.Tutor.MyAppUser.LastName,
				TutorId = l.TutorID
			})
			.ToList();

		if (!lessons.Any())
			return NotFound(new { message = "Nema lekcija za ovu sedmicu." });

		return Ok(lessons);
	}

	[HttpPost]
	public IActionResult AddLesson([FromBody] LessonCreateDTO dto)
	{
		if (dto == null)
			return BadRequest("Podaci o lekciji nisu poslani.");

		if (!DateTime.TryParse(dto.Date, out DateTime date))
			return BadRequest("Datum nije ispravan. Očekivan format: dd-MM-yyyy.");

		if (!TimeSpan.TryParse(dto.Start, out TimeSpan startTime))
			return BadRequest("Vrijeme početka nije ispravno. Očekivan format: HH:mm.");

		if (!TimeSpan.TryParse(dto.End, out TimeSpan endTime))
			return BadRequest("Vrijeme završetka nije ispravno. Očekivan format: HH:mm.");

		var startDateTime = date.Date + startTime;
		var endDateTime = date.Date + endTime;

		if (endDateTime <= startDateTime)
			return BadRequest("Vrijeme završetka mora biti nakon početka.");

		if (startDateTime <= DateTime.Now)
			return BadRequest("Lekcija se može zakazati samo u budućnosti.");

		// Provjera da tutor predaje predmet
		bool tutorPredajePredmet = db.TutorsSubjects.Any(ts =>
			ts.TutorID == dto.TutorID &&
			ts.SubjectID == dto.SubjectID);

		if (!tutorPredajePredmet)
			return BadRequest("Tutor ne predaje izabrani predmet.");

		// Provjera da tutor nije zauzet
		bool tutorConflict = db.Lessons.Any(l =>
			l.TutorID == dto.TutorID &&
			l.Status == LessonStatus.Scheduled &&
			l.StartTime < endDateTime &&
			startDateTime < l.EndTime);

		if (tutorConflict)
			return Conflict("Tutor već ima zakazanu lekciju u tom terminu.");

		// Provjera da student nije zauzet
		bool studentConflict = db.Lessons.Any(l =>
			l.StudentID == dto.StudentID &&
			l.Status == LessonStatus.Scheduled &&
			l.StartTime < endDateTime &&
			startDateTime < l.EndTime);

		if (studentConflict)
			return Conflict("Student već ima zakazanu lekciju u tom terminu.");

		var lesson = new Lesson
		{
			StudentID = dto.StudentID,
			TutorID = dto.TutorID,
			SubjectID = dto.SubjectID,
			StartTime = startDateTime,
			EndTime = endDateTime,
			LessonDate = date.Date,
			Status = LessonStatus.Scheduled,
			Mode = dto.LessonMode
		};

		db.Lessons.Add(lesson);
		db.SaveChanges();

		var student = db.Students
			.Include(s => s.MyAppUser)
			.FirstOrDefault(s => s.ID == dto.StudentID);

		var tutor = db.Tutors
			.Include(t => t.MyAppUser)
			.FirstOrDefault(t => t.ID == dto.TutorID);

		var subject = db.Subjects.FirstOrDefault(s => s.ID == dto.SubjectID);

		return Ok(new
		{
			message = "Lekcija je uspješno zakazana.",
			lesson = new
			{
				lesson.ID,
				Datum = lesson.LessonDate.ToString("yyyy-MM-dd"),
				VrijemeOd = lesson.StartTime.ToString("HH:mm"),
				VrijemeDo = lesson.EndTime.ToString("HH:mm"),
				Tutor = tutor?.MyAppUser.FirstName + " " + tutor?.MyAppUser.LastName,
				Student = student?.MyAppUser.FirstName + " " + student?.MyAppUser.LastName,
				Predmet = subject?.Name,
				Mode = lesson.Mode.ToString(),
				Status = lesson.Status.ToString()
			}
		});
	}


	[HttpDelete("{id}")]
	public IActionResult DeleteLesson(int id)
	{
		var lesson = db.Lessons.Find(id);
		if (lesson == null)
			return NotFound();

		db.Lessons.Remove(lesson);
		db.SaveChanges();

		return NoContent();
	}
}
