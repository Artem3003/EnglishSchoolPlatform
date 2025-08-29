using Domain.Entities.Common;

namespace Domain.Entities;

public class Student : BaseEntity
{
    public Guid UserId { get; set; }
    public string? Level { get; set; }
    public DateTime? DateOfBirth { get; set; }

    // Navigation property
    public User User { get; set; } = null!;
}
