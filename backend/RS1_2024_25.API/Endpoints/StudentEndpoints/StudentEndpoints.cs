using Microsoft.AspNetCore.Mvc;
using RS1_2024_2025.API.Endpoints.MyAppUserEndpoints;
using RS1_2024_2025.API;
using RS1_2024_2025.Domain.Entities;
using RS1_2024_2025.Database;
using AutoMapper;
using RS1_2024_2025.Domain.Requests;

namespace RS1_2024_25.API.Endpoints.StudentEndpoints
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentEndpoints(ApplicationDbContext db, IMapper mapper):ControllerBase
	{
		[HttpGet]
		public IActionResult GetAll()
		{
			var students = db.Students
				.Select(s => new Student
				{
					ID = s.ID,
					Grade=s.Grade,
					PreferredMode=s.PreferredMode,
					EducationLevel=s.EducationLevel
				})
				.ToArray();

			return Ok(students);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var students = db.Students
				.Where(s => s.ID == id)
				.Select(s => new Student
				{
					ID = s.ID,
					Grade = s.Grade,
					PreferredMode = s.PreferredMode,
					EducationLevel = s.EducationLevel
				})
				.FirstOrDefault();

			if (students == null)
			{
				return NotFound();
			}

			return Ok(students);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] StudentInsertRequest request, CancellationToken cancellationToken)
		
		{
			MyAppUser newUser = mapper.Map<MyAppUser>(request);
			Student newStudent = mapper.Map<Student>(request);
			newStudent.MyAppUser = newUser;
			db.MyAppUsers.Add(newUser);
			db.Students.Add(newStudent);
			await db.SaveChangesAsync(cancellationToken);

			return Ok(newStudent);
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] StudentUpdateRequest request)
		{
			var student = db.Students.FirstOrDefault(s => s.ID == id);
			if (student == null)
			{
				return NotFound();
			}
			mapper.Map(request, student.MyAppUser);
			mapper.Map(request, student);
			await db.SaveChangesAsync();
			return Ok(student);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult >Delete(int studentId)
		{
			var student = db.Students.Where(x => x.ID == studentId).FirstOrDefault();
			if (student == null)
			{
				return NotFound();
			}
			else
			{
				db.MyAppUsers.Remove(student.MyAppUser);
				db.Students.Remove(student);
				await db.SaveChangesAsync();
				return Ok(student);
			}
		}
	}
}

