namespace Application.DTOs.HomeworkAssignment;

public class HomeworkAssignmentDto
{
    public Guid Id { get; set; }

    public Guid HomeworkId { get; set; }

    public string StudentId { get; set; }

    public string SubmissionText { get; set; }

    public string AttachmentUrl { get; set; }

    public int? Grade { get; set; }

    public string TeacherFeedback { get; set; }
}
