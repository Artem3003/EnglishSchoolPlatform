namespace Application.DTOs.Course;

public class CourseDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public double Price { get; set; }

    public int NumberOfLessons { get; set; }
}
