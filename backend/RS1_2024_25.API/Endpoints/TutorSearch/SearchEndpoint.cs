using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RS1_2024_25.API.Endpoints.TutorSearch
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchEndpoint : ControllerBase
    {
        private readonly ISearchService _tutorService;
        public SearchEndpoint(ISearchService tutorService) { 
            _tutorService = tutorService;
        }
        [HttpPost("search-tutors")]
        public async Task<IActionResult> SearchTutors([FromBody] TutorSearchDTO searchDTO)
        {
            var results = await _tutorService.SearchTutors(searchDTO);
            return Ok(results);
        }
    }
}
