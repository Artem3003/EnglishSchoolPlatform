using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Course;

public class CreateCourseDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(255, ErrorMessage = "Title cannot exceed 255 characters")]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
    public double Price { get; set; }

    [Required(ErrorMessage = "Number of lessons is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Number of lessons must be at least 1")]
    public int NumberOfLessons { get; set; }
}
