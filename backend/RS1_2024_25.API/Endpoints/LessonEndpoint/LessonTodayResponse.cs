namespace RS1_2024_25.API.Endpoints.LessonEndpoint
{
    public class LessonTodayResponse
    {
        public int LessonID { get; set; }
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Mode { get; set; }
    }
}
