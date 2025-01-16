using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_25.API.Endpoints.TutorSearch
{
    public interface ISearchService
    {
        public Task<List<TutorSearchResultDTO>> SearchTutors(TutorSearchDTO searchDTO);
    }
}
