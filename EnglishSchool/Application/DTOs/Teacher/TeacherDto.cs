using Application.DTOs.User;

namespace Application.DTOs.Teacher;

public class TeacherDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public int? ExperienceYears { get; set; }
    public UserDto User { get; set; } = null!;
}
