using Application.DTOs.Course;

namespace Application.Interfaces;

public interface ICourseService
{
    Task<Guid> CreateCourseAsync(CreateCourseDto dto);

    Task<CourseDto> GetCourseByIdAsync(Guid id);

    Task<CourseDto?> GetCourseByTitleAsync(string title);

    Task<IEnumerable<CourseDto>> GetAllCoursesAsync();

    Task<IEnumerable<CourseDto>> GetAvailableCoursesAsync(Guid? excludeLessonId = null);

    Task UpdateCourseAsync(UpdateCourseDto dto);

    Task DeleteCourseAsync(Guid id);
}
