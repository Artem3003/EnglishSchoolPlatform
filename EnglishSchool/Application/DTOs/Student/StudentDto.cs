using Application.DTOs.User;

namespace Application.DTOs.Student;

public class StudentDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Level { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public UserDto User { get; set; } = null!;
}
