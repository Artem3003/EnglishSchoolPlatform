using Application.DTOs.User;

namespace Application.DTOs.Admin;

public class AdminDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserDto User { get; set; } = null!;
}
