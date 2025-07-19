using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_2025.API.Endpoints.ReservationEndpoints;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.Entities;
using System.Text.Json;
using Xunit;
using RS1_2024_25.API.Endpoints.ReservationEndpoints;

public class ReservationEndpointTests
{
    private async Task<ApplicationDbContext> GetDbContextWithTestData()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);

        var studentUser = new MyAppUser
        {
            FirstName = "Test",
            LastName = "Student",
            Email = "student@test.com",
            Password = "test123",
            PhoneNumber = "123456789",
            Username = "studentuser",
            UserType = UserType.Student
        };

        var tutorUser = new MyAppUser
        {
            FirstName = "Test",
            LastName = "Tutor",
            Email = "tutor@test.com",
            Password = "test123",
            PhoneNumber = "987654321",
            Username = "tutoruser",
            UserType = UserType.Tutor
        };

        var student = new Student
        {
            MyAppUser = studentUser,
            Grade = "2",
            PreferredMode = "Online"
        };

        var tutor = new Tutor { 
            MyAppUser = tutorUser,
            Qualifications="Bachelor of IT",
            YearsOfExperience=2,
            Availability="Only at weekend",
            Rating=4.5,
            Policy="Cancelling is not possible",
            HourlyRate="35KM/h",
            IsLiveAvailable=false
        };
        var subject = new Subject
        {
            Name = "Math",
            Description = "Mathematics subject for all levels.",
            DifficultyLevel = "High school"
        };

        var lesson = new Lesson
        {
            Subject = subject,
            Student = student,
            Tutor = tutor,
            LessonDate = DateTime.Now,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddMinutes(45),
            Status = LessonStatus.Scheduled,
            Mode = LessonMode.Online
        };

        var reservation = new Reservation
        {
            Tutor = tutor,
            Student = student,
            Lesson = lesson,
            ReservationDate = DateTime.Now,
            Status = ReservationStatus.Pending
        };

        await context.AddAsync(subject);
        await context.AddAsync(lesson);
        await context.AddAsync(tutor);
        await context.AddAsync(student);
        await context.AddAsync(reservation);
        await context.SaveChangesAsync();

        return context;
    }



    [Fact]
    public async Task ApproveReservation_Should_Return_Ok_When_Valid()
    {
        // Arrange
        var dbContext = await GetDbContextWithTestData();
        var endpoint = new ReservationEndpoint(dbContext);

        var reservationId = await dbContext.Reservations
            .Select(r => r.ID)
            .FirstAsync();

        // Act
        var result = await endpoint.ApproveReservation(reservationId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<MessageResponse>(okResult.Value);
        Assert.Equal("Reservation approved successfully.", response.Message);



    }

    [Fact]
    public async Task ApproveReservation_Should_Return_BadRequest_When_AlreadyApproved()
    {
        // Arrange
        var dbContext = await GetDbContextWithTestData();
        var reservation = await dbContext.Reservations.FirstAsync();

        // Ručno postavi status na Approved
        reservation.Status = ReservationStatus.Approved;
        await dbContext.SaveChangesAsync();

        var endpoint = new ReservationEndpoint(dbContext);

        // Act
        var result = await endpoint.ApproveReservation(reservation.ID);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Only pending reservations can be approved.", badRequest.Value);
    }
}

