namespace Application.DTOs.Lesson;

public class UpdateLessonDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }
}
