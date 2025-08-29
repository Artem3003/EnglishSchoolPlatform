namespace Application.DTOs.Teacher;

public class UpdateTeacherDto
{
    public Guid Id { get; set; }
    public string Subject { get; set; } = string.Empty;
    public int? ExperienceYears { get; set; }
}
