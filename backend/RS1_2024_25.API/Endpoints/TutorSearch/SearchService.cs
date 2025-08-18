using Microsoft.EntityFrameworkCore;
using MiNET.Plugins;
using MiNET.UI;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_25.API.Endpoints.TutorSearch
{
    public class SearchService:ISearchService
    {
        private readonly ApplicationDbContext _dbContext;
        

        public SearchService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<TutorSearchResultDTO>> SearchTutors(TutorSearchDTO searchDTO)
        {
            var tutors = _dbContext.Tutors
                .Include(t => t.MyAppUser)
                .ThenInclude(ts => ts.City)
                .Include(t => t.TutorSubjects)
                .ThenInclude(ts => ts.Subject)
                .AsQueryable();

            if (searchDTO.SubjectId.HasValue)
                tutors = tutors.Where(t => t.TutorSubjects.Any(ts => ts.SubjectID == searchDTO.SubjectId));

            if (searchDTO.CategoryId.HasValue)
                tutors = tutors.Where(t => t.TutorSubjects.Any(ts => ts.Subject.SubjectCategories.Any(sc => sc.CategoryID == searchDTO.CategoryId)));

            if (searchDTO.CityId.HasValue) 
                tutors = tutors.Where(t => t.MyAppUser.CityId == searchDTO.CityId);
                
            

            if (searchDTO.InPersonAvailable.HasValue && searchDTO.InPersonAvailable.Value)
                tutors = tutors.Where(t => t.Availability.Contains("Uživo", StringComparison.OrdinalIgnoreCase));

            
            var tutorList = await tutors.ToListAsync();


            if (searchDTO.MinPrice.HasValue)
            {
                tutorList = tutorList.Where(t =>
                {
                    var numericPart = t.HourlyRate.Substring(0, t.HourlyRate.IndexOf("KM"));
                    return double.TryParse(numericPart, out var rate) && rate >= searchDTO.MinPrice.Value;
                }).ToList();
            }

            if (searchDTO.MaxPrice.HasValue)
            {
                tutorList = tutorList.Where(t =>
                {
                    var numericPart = t.HourlyRate.Substring(0, t.HourlyRate.IndexOf("KM"));
                    return double.TryParse(numericPart, out var rate) && rate <= searchDTO.MaxPrice.Value;
                }).ToList();
            }



         return tutorList
        .OrderByDescending(t => t.Rating)
        .Select(t => new TutorSearchResultDTO
        {
            ID=t.ID,
            Name = t.MyAppUser.FirstName + ' ' + t.MyAppUser.LastName,
            City = t.MyAppUser.City != null ? t.MyAppUser.City.Name :"unknown",
            Rating = t.Rating,
            HourlyRate = t.HourlyRate,
            ProfileImageUrl=t.MyAppUser.ProfileImageUrl
        }).ToList();
        }


    }

}

