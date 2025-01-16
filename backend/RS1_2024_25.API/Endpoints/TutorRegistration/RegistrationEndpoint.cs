using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_2025.API;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.Entities;
using RS1_2024_25.API.Endpoints.TutorRegistration.DTOs;

namespace RS1_2024_25.API.Endpoints.TutorRegistration
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationEndpoint : ControllerBase
    {
        [HttpPost("register-tutor")]
        public async Task<IActionResult> RegisterTutor([FromBody] TutorRegistrationDto registrationDto, ApplicationDbContext db)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var existingUser = await db.MyAppUsers
                .FirstOrDefaultAsync(u => u.Username.ToLower() == registrationDto.Username.ToLower());

            if (existingUser != null)
            {
                return Conflict(new { message = "Username is taken." });
            }

            if (!string.IsNullOrEmpty(registrationDto.ProfileImageUrl))
            {

                var imageBytes = Convert.FromBase64String(registrationDto.ProfileImageUrl);
                string imageUrl = await UploadImageHelper.UploadFile(imageBytes);
                registrationDto.ProfileImageUrl = imageUrl;
            }

            var myAppUser = new MyAppUser
            {
                Username = registrationDto.Username,
                Password = registrationDto.Password,
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                Age = registrationDto.Age,
                Email = registrationDto.Email,
                PhoneNumber = registrationDto.PhoneNumber,
                UserType = UserType.Tutor,
                CityId = registrationDto.CityId,
                Gender = registrationDto.Gender,
                ProfileImageUrl = registrationDto.ProfileImageUrl
            };

            db.MyAppUsers.Add(myAppUser);
            await db.SaveChangesAsync();


            var tutor = new Tutor
            {
                MyAppUserID = myAppUser.ID,
                Qualifications = registrationDto.Qualifications,
                YearsOfExperience = registrationDto.YearsOfExperience,
                Availability = registrationDto.Availability,
                Policy = registrationDto.Policy,
                HourlyRate = registrationDto.HourlyRate,
                IsLiveAvailable = registrationDto.IsLiveAvailable
            };

            db.Tutors.Add(tutor);
            await db.SaveChangesAsync();

            return Ok(new { message = "Successful registration!" });
        }

    }
}
