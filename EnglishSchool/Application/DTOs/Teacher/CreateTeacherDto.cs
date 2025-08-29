namespace Application.DTOs.Teacher;

public class CreateTeacherDto
{
    public Guid UserId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public int? ExperienceYears { get; set; }
}
