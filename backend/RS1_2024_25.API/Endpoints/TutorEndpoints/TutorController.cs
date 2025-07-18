using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_2025.Database;
using RS1_2024_25.API.Endpoints.TutorEndpoints.DTOs;

namespace RS1_2024_2025.API.Endpoints.TutorEndpoints;

[Route("api/[controller]")]
[ApiController]
public class TutorEndpoint(ApplicationDbContext db) : ControllerBase
{
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTutorProfile(
        int id,
        [FromBody] TutorProfileDto dto,
        CancellationToken cancellationToken = default)
    {
        var tutor = await db.Tutors
            .Include(t => t.MyAppUser)
            .FirstOrDefaultAsync(t => t.ID == id, cancellationToken);

        if (tutor == null)
            return NotFound("Tutor not found.");

        // Update podataka iz MyAppUser
        tutor.MyAppUser.FirstName = dto.FirstName;
        tutor.MyAppUser.LastName = dto.LastName;
        tutor.MyAppUser.Username = dto.Username;
        tutor.MyAppUser.Email = dto.Email;
        tutor.MyAppUser.PhoneNumber = dto.PhoneNumber;
        tutor.MyAppUser.Gender = dto.Gender;
        tutor.MyAppUser.CityId = dto.CityId;
        

        // Update podataka iz Tutor modela
        tutor.Qualifications = dto.Qualifications;
        tutor.YearsOfExperience = dto.YearsOfExperience;
        tutor.Availability = dto.Availability;
        tutor.Policy = dto.Policy;
        tutor.HourlyRate = dto.HourlyRate;
        tutor.IsLiveAvailable = dto.IsLiveAvailable;

        await db.SaveChangesAsync(cancellationToken);

        return Ok(new { message = "Tutor profile updated successfully." });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTutorProfile(int id, CancellationToken cancellationToken = default)
    {
        var tutor = await db.Tutors
            .Include(t => t.MyAppUser)
            .FirstOrDefaultAsync(t => t.ID == id, cancellationToken);

        if (tutor == null)
            return NotFound("Tutor not found.");

        var dto = new TutorProfileDto
        {
            TutorID = tutor.ID,
            FirstName = tutor.MyAppUser.FirstName,
            LastName = tutor.MyAppUser.LastName,
            Username = tutor.MyAppUser.Username,
            Email = tutor.MyAppUser.Email,
            PhoneNumber = tutor.MyAppUser.PhoneNumber,
            Gender = tutor.MyAppUser.Gender,
            CityId = tutor.MyAppUser.CityId,
            ProfileImageUrl = tutor.MyAppUser.ProfileImageUrl,

            Qualifications = tutor.Qualifications,
            YearsOfExperience = tutor.YearsOfExperience,
            Availability = tutor.Availability,
            Policy = tutor.Policy,
            HourlyRate = tutor.HourlyRate,
            IsLiveAvailable = tutor.IsLiveAvailable
        };

        return Ok(dto);
    }

    [HttpGet("check-username")]
    public async Task<IActionResult> CheckUsername(string username, int excludeTutorId)
    {
        var isTaken = await db.MyAppUsers
            .Where(u => u.Username.ToLower() == username.ToLower())
            .Where(u => !db.Tutors.Any(t => t.ID == excludeTutorId && t.ID == u.ID))
            .AnyAsync();

        return Ok(new { isTaken });
    }



    [HttpPost("{id}/upload-profile-image")]
    public async Task<IActionResult> UploadProfileImage(int id, [FromBody] UploadImageDto dto, CancellationToken cancellationToken)
    {
        var tutor = await db.Tutors
            .Include(t => t.MyAppUser)
            .FirstOrDefaultAsync(t => t.ID == id, cancellationToken);

        if (tutor == null)
            return NotFound("Tutor not found.");

        byte[] imageBytes = Convert.FromBase64String(dto.Base64);

        var imageUrl = await UploadImageHelper.UploadFile(imageBytes);
        tutor.MyAppUser.ProfileImageUrl = imageUrl;

        await db.SaveChangesAsync(cancellationToken);
        return Ok(new { ImageUrl = imageUrl });
    }

    public class UploadImageDto
    {
        public string Base64 { get; set; } = string.Empty;
    }


}
