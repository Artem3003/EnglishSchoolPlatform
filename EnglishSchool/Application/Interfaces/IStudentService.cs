using Application.DTOs.Student;

namespace Application.Interfaces;

public interface IStudentService
{
    Task<StudentDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<StudentDto>> GetAllAsync();
    Task<StudentDto> CreateAsync(CreateStudentDto createStudentDto);
    Task<StudentDto> UpdateAsync(UpdateStudentDto updateStudentDto);
    Task<bool> DeleteAsync(Guid id);
}
