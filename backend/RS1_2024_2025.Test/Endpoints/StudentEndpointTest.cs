using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.Entities;
using RS1_2024_2025.Domain.Requests;
using RS1_2024_25.API.Endpoints.StudentEndpoints;

namespace RS1_2024_2025.Test.Endpoints
{
	public class StudentEndpointTest
	{
		private readonly ApplicationDbContext _dbContext;

		private readonly IMapper _mapper;

		public StudentEndpointTest()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;

			_dbContext = new ApplicationDbContext(options);

			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<StudentInsertRequest, MyAppUser>();
				cfg.CreateMap<StudentInsertRequest, Student>();
			});

			_mapper = config.CreateMapper();

			SeedTestData().Wait();
		}

		private async Task SeedTestData()
		{
			var user = new MyAppUser
			{
				Username = "studentuser",
				FirstName = "Test",
				LastName = "Student",
				UserType = UserType.Student,
				Email = "student@test.com",         
				Password = "test123",                
				PhoneNumber = "123456789"
			};

			var student = new Student
			{
				MyAppUser = user,
				Grade = "3",
				PreferredMode = "Online",
				EducationLevel = EducationLevel.HighSchool
			};

			await _dbContext.MyAppUsers.AddAsync(user);
			await _dbContext.Students.AddAsync(student);
			await _dbContext.SaveChangesAsync();
		}

		[Fact]
		public void GetById_Should_Return_Student_When_Exists()
		{
			//Arrange
			var endpoint = new StudentEndpoints(_dbContext, _mapper, null);
			var existingId = _dbContext.Students.First().ID;

			//Act
			var result = endpoint.GetById(existingId);

			//Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var student = Assert.IsType<Student>(okResult.Value);
			Assert.Equal(existingId, student.ID);
		}

		[Fact]
		public async Task Post_Should_Return_BadRequest_When_Username_Exists()
		{
			//Arrange
			var endpoint = new StudentEndpoints(_dbContext, _mapper, null);

			var request = new StudentInsertRequest
			{
				Username = "studentuser", //već postoji u bazi
				FirstName = "New",
				LastName = "Student",
				Password = "pass123",
				Email = "new@student.com",
				Grade = "4",
				PreferredMode = "Online",
				EducationLevel = EducationLevel.HighSchool
			};

			//Act
			var result = await endpoint.Post(request, default);

			//Assert
			var badRequest = Assert.IsType<BadRequestObjectResult>(result);
			var message = badRequest.Value?.GetType().GetProperty("message")?.GetValue(badRequest.Value, null)?.ToString();
			Assert.Equal("Korisničko ime je već zauzeto.", message);
		}
	}
}
