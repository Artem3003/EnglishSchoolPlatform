using Domain.Entities.Common;

namespace Domain.Entities;

public class Course : BaseEntity<Guid>
{
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public double Price { get; set; }

    public int NumberOfLessons { get; set; }

    public List<Lesson> Lessons { get; set; } = [];
}
