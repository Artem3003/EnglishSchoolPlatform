using Domain.Entities.Common;

namespace Domain.Entities;

public class Teacher : BaseEntity
{
    public Guid UserId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public int? ExperienceYears { get; set; }

    // Navigation property
    public User User { get; set; } = null!;
}
