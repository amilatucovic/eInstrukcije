using Microsoft.AspNetCore.Mvc;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.Entities;
using RS1_2024_2025.Domain.Entities.Models;
using System.Linq;

namespace RS1_2024_2025.API.Endpoints.SubjectEndpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectGetEndpoint : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public SubjectGetEndpoint(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var subjects = db.Subjects
                .Select(s => new
                {
                    s.ID,
                    s.Name,
                    s.Description,
                    s.DifficultyLevel,
                    Categories = s.SubjectCategories.Select(sc => new
                    {
                        sc.Category.ID,
                        sc.Category.Name
                    }).ToArray() 
                })
                .ToArray();

            return Ok(subjects);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var subject = db.Subjects
                .Where(s => s.ID == id)
                .Select(s => new
                {
                    s.ID,
                    s.Name,
                    s.Description,
                    s.DifficultyLevel,
                    Categories = s.SubjectCategories.Select(sc => new
                    {
                        sc.Category.ID,
                        sc.Category.Name
                    }).ToArray() 
                })
                .FirstOrDefault();

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        [HttpPost]
        public IActionResult Post([FromBody] SubjectRequest subjectRequest)
        {
            var subject = new Subject
            {
                Name = subjectRequest.Name,
                Description = subjectRequest.Description,
                DifficultyLevel = subjectRequest.DifficultyLevel
            };

            db.Subjects.Add(subject);
            db.SaveChanges();

            var subjectResponse = new SubjectResponse
            {
                ID = subject.ID,
                Name = subject.Name,
                Description = subject.Description,
                DifficultyLevel = subject.DifficultyLevel
            };

            return Ok(subjectResponse);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SubjectRequest subjectRequest)
        {
            var subject = db.Subjects.FirstOrDefault(s => s.ID == id);
            if (subject == null)
            {
                return NotFound();
            }

            subject.Name = subjectRequest.Name;
            subject.Description = subjectRequest.Description;
            subject.DifficultyLevel = subjectRequest.DifficultyLevel;

            db.SaveChanges();

            var subjectResponse = new SubjectResponse
            {
                ID = subject.ID,
                Name = subject.Name,
                Description = subject.Description,
                DifficultyLevel = subject.DifficultyLevel
            };

            return Ok(subjectResponse);
        }
        [HttpGet("categories/{subjectId}")]
        public IActionResult GetCategoriesBySubjectId(int subjectId)
        {
            var subject = db.Subjects
                .Where(s => s.ID == subjectId)
                .Select(s => new
                {
                    Categories = s.SubjectCategories.Select(sc => new
                    {
                        sc.Category.ID,
                        sc.Category.Name
                    }).ToArray()
                })
                .FirstOrDefault();

            if (subject == null || subject.Categories.Length == 0)
            {
                return NotFound("No categories found for the given subject.");
            }

            return Ok(subject.Categories);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var subject = db.Subjects.Where(x => x.ID == id).FirstOrDefault();
            if (subject == null)
            {
                return NotFound();
            }
            else
            {
                db.Subjects.Remove(subject);
                db.SaveChanges();
                return Ok();
            }
        }
    }
}
