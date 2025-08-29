namespace Application.DTOs.Student;

public class UpdateStudentDto
{
    public Guid Id { get; set; }
    public string? Level { get; set; }
    public DateTime? DateOfBirth { get; set; }
}
