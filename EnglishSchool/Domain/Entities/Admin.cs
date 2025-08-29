using Domain.Entities.Common;

namespace Domain.Entities;

public class Admin : BaseEntity
{
    public Guid UserId { get; set; }

    // Navigation property
    public User User { get; set; } = null!;
}
