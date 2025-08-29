using Application.DTOs.Teacher;

namespace Application.Interfaces;

public interface ITeacherService
{
    Task<TeacherDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<TeacherDto>> GetAllAsync();
    Task<TeacherDto> CreateAsync(CreateTeacherDto createTeacherDto);
    Task<TeacherDto> UpdateAsync(UpdateTeacherDto updateTeacherDto);
    Task<bool> DeleteAsync(Guid id);
}
