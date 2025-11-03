using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Course;

public class UpdateCourseDto
{
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }

    [StringLength(255, ErrorMessage = "Title cannot exceed 255 characters")]
    public string? Title { get; set; }

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
    public double? Price { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Number of lessons must be at least 1")]
    public int? NumberOfLessons { get; set; }
}
