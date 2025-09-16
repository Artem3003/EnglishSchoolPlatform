namespace Application.DTOs.Homework;

public class HomeworkDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Instructions { get; set; } = string.Empty;

    public DateTime DueDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? LessonId { get; set; }
}
