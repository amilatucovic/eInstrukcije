using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.Entities;
using RS1_2024_25.API.Endpoints.ReservationEndpoints;

namespace RS1_2024_2025.API.Endpoints.ReservationEndpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationEndpoint(ApplicationDbContext db) : ControllerBase
    {
        [HttpGet("tutor/{tutorId}")]
        public async Task<IActionResult> GetTutorReservations(
            int tutorId,
            [FromQuery] ReservationStatus? status,
            [FromQuery] DateTime? date,
            [FromQuery] string? search,
            CancellationToken cancellationToken = default)
        {
            var query = db.Reservations
                .Include(r => r.Student)
                .Include(r => r.Lesson)
                .Where(r => r.TutorID == tutorId)
                .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(r => r.Status == status.Value);
            }

            if (date.HasValue)
            {
                query = query.Where(r => r.ReservationDate.Date == date.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowered = search.ToLower();
                query = query.Where(r =>
                    (r.Student.MyAppUser.FirstName + " " + r.Student.MyAppUser.LastName).ToLower().Contains(lowered));
            }

            var result = await query
                .OrderByDescending(r => r.ReservationDate)
                .Select(r => new ReservationDto
                {
                    Id = r.ID,
                    StudentFullName = r.Student.MyAppUser.FirstName + " " + r.Student.MyAppUser.LastName,
                    LessonName = r.Lesson.Subject.Name, 
                    ReservationDate = r.ReservationDate,
                    Status = r.Status
                }).ToListAsync(cancellationToken);


            return Ok(result);
        }

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveReservation(int id, CancellationToken cancellationToken = default)
        {
            var reservation = await db.Reservations.FirstOrDefaultAsync(r => r.ID == id, cancellationToken);
            if (reservation == null)
                return NotFound("Reservation not found.");

            if (reservation.Status != ReservationStatus.Pending)
                return BadRequest("Only pending reservations can be approved.");

            reservation.Status = ReservationStatus.Approved;
            reservation.ApprovedDate = DateTime.Now;

            await db.SaveChangesAsync(cancellationToken);

            return Ok(new { Message = "Reservation approved successfully." });
        }

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectReservation(int id, CancellationToken cancellationToken = default)
        {
            var reservation = await db.Reservations.FirstOrDefaultAsync(r => r.ID == id, cancellationToken);
            if (reservation == null)
                return NotFound("Reservation not found.");

            if (reservation.Status != ReservationStatus.Pending)
                return BadRequest("Only pending reservations can be rejected.");

            reservation.Status = ReservationStatus.Rejected;
            reservation.RejectedDate = DateTime.Now;

            await db.SaveChangesAsync(cancellationToken);

            return Ok(new { Message = "Reservation rejected successfully." });
        }


    }
}
