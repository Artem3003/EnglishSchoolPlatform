namespace Application.DTOs.Homework;

public class CreateHomeworkDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string Instructions { get; set; }

    public DateTime DueDate { get; set; }

    public string TeacherId { get; set; }

    public Guid LessonId { get; set; }
}
