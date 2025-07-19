using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_25.API.Endpoints.ReservationEndpoints
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public string StudentFullName { get; set; }
        public string LessonName { get; set; }
        public DateTime ReservationDate { get; set; }
        public ReservationStatus Status { get; set; }
    }

    public class MessageResponse
    {
        public string Message { get; set; }
    }

}
