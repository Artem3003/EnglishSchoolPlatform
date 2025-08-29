namespace Application.DTOs.Student;

public class CreateStudentDto
{
    public Guid UserId { get; set; }
    public string? Level { get; set; }
    public DateTime? DateOfBirth { get; set; }
}
