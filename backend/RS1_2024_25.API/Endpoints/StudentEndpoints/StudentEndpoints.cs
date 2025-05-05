using Microsoft.AspNetCore.Mvc;
using RS1_2024_2025.Domain.Entities;
using RS1_2024_2025.Database;
using AutoMapper;
using RS1_2024_2025.Domain.Requests;
using RS1_2024_2025.Domain.SearchObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace RS1_2024_25.API.Endpoints.StudentEndpoints
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentEndpoints(ApplicationDbContext db, IMapper mapper) : ControllerBase
	{
		[HttpGet]
		public IActionResult GetAll([FromQuery] StudentSearchObject? search)
		{
			var query = db.Set<Student>().AsQueryable();
			if (search?.FirstName.IsNullOrEmpty() == false)
			{
				query = query.Where(s => s.MyAppUser.FirstName.StartsWith(search.FirstName));
			}
			if (search?.LastName.IsNullOrEmpty() == false)
			{
				query = query.Where(s => s.MyAppUser.LastName.StartsWith(search.LastName));
			}
			if (search?.Grade.IsNullOrEmpty() == false)
			{
				query = query.Where(s => s.Grade == search.Grade);
			}
			if (search?.EducationLevel != null)
			{
				query = query.Where(s => s.EducationLevel == search.EducationLevel);
			}
			if (search?.PrefferedMode.IsNullOrEmpty() == false)
			{
				query = query.Where(s => s.PreferredMode.Contains(search.PrefferedMode));
			}

			if (search?.CityId != null)
			{
				query = query.Where(s => s.MyAppUser.CityId == search.CityId);
			}

			if (search?.IsUserIncluded == true)
			{
				query = query.Include("MyAppUser");
				query = query.Include("MyAppUser.City");
			}

			var students = query.ToList();
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
			newUser.UserType = UserType.Student;
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
			var student = db.Students.Include(s=>s.MyAppUser).FirstOrDefault(s => s.ID == id);
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
		public async Task<IActionResult> Delete(int id)
		{
			var student = db.Students.Where(x => x.ID == id).FirstOrDefault();
			if (student == null)
			{
				return NotFound();
			}
			var user = db.MyAppUsers.Find(student.MyAppUserID);
			db.Students.Remove(student);
			db.MyAppUsers.Remove(user);
			await db.SaveChangesAsync();
			return Ok(student);
		}
	}
}

