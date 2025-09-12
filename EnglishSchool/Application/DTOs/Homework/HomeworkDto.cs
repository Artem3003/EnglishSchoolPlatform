namespace Application.DTOs.Homework;

public class HomeworkDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Instructions { get; set; }

    public DateTime DueDate { get; set; }

    public string TeacherId { get; set; }

    public Guid LessonId { get; set; }
}
