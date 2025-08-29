namespace Application.DTOs.User;

public class UpdateUserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
}
