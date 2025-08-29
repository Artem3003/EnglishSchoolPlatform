using Domain.Entities.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Admin? Admin { get; set; }
    public Teacher? Teacher { get; set; }
    public Student? Student { get; set; }
}
