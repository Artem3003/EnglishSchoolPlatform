namespace Application.DTOs.Lesson;

public class CreateLessonDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime ScheduledDateTime { get; set; }

    public int DurationMinutes { get; set; }

    public string TeacherId { get; set; }

    public string Type { get; set; }

    public string Status { get; set; }
}
