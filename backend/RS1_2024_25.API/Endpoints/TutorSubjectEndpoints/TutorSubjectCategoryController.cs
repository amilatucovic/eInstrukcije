using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace RS1_2024_25.API.Endpoints.TutorSubjectEndpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorSubjectCategoryController(ApplicationDbContext db) : ControllerBase
    {

        [HttpGet("{tutorId}")]
        public async Task<IActionResult> GetByTutorId(int tutorId, CancellationToken cancellationToken = default)
        {
            var result = await db.TutorSubjectCategories
                .Where(x => x.TutorID == tutorId)
                .Select(x => new
                {
                    x.SubjectID,
                    SubjectName = x.Subject.Name,
                    x.CategoryID,
                    CategoryName = x.Category.Name
                })
                .ToListAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedByTutorId(
    [FromQuery] int tutorId,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 5,
    CancellationToken cancellationToken = default)
        {
            var query = db.TutorSubjectCategories
                .Where(x => x.TutorID == tutorId);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new
                {
                    x.SubjectID,
                    SubjectName = x.Subject.Name,
                    x.CategoryID,
                    CategoryName = x.Category.Name,
                    x.TutorID
                })
                .ToListAsync(cancellationToken);

            return Ok(new
            {
                totalCount,
                items
            });
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TutorSubjectCategoryDto dto, CancellationToken cancellationToken = default)
        {
            var exists = await db.TutorSubjectCategories.AnyAsync(
                x => x.TutorID == dto.TutorID && x.SubjectID == dto.SubjectID && x.CategoryID == dto.CategoryID,
                cancellationToken);

            if (exists)
                return BadRequest("Tutor already teaches this subject category.");

            var entity = new TutorSubjectCategory
            {
                TutorID = dto.TutorID,
                SubjectID = dto.SubjectID,
                CategoryID = dto.CategoryID
            };

            db.TutorSubjectCategories.Add(entity);
            await db.SaveChangesAsync(cancellationToken);

            return Ok(new { Message = "Subject category successfully added to tutor." });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(
            [FromQuery] int tutorId,
            [FromQuery] int subjectId,
            [FromQuery] int categoryId,
            CancellationToken cancellationToken = default)
        {
            var entity = await db.TutorSubjectCategories.FirstOrDefaultAsync(
                x => x.TutorID == tutorId && x.SubjectID == subjectId && x.CategoryID == categoryId,
                cancellationToken);

            if (entity == null)
                return NotFound("Tutor does not teach this subject category.");

            db.TutorSubjectCategories.Remove(entity);
            await db.SaveChangesAsync(cancellationToken);

            return Ok(new { Message = "Subject category removed from tutor." });
        }
    }
}

