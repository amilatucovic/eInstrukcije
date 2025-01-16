namespace RS1_2024_25.API.Endpoints.TutorSearch
{
    public class TutorSearchDTO
    {
        public int? SubjectId { get; set; }
        public int? CategoryId { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int? CityId { get; set; }
        public bool? InPersonAvailable { get; set; }
    }
}
