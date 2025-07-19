using RS1_2024_2025.Domain.Entities;
using System.Text.Json.Serialization;

public class LessonScheduleDTO
{
    public int LessonID { get; set; }
    public string SubjectName { get; set; }
	public int SubjectID { get; set; }
	public int StudentId {  get; set; }
    public int TutorId {  get; set; }
    public string StudentName { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LessonMode LessonMode { get; set; }
    public string Status { get; set; }
    public string TutorName { get; set; }
}
