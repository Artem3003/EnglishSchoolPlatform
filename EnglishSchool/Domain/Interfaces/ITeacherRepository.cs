using Domain.Entities;

namespace Domain.Interfaces;

public interface ITeacherRepository
{
    Task<Teacher?> GetByIdAsync(Guid id);
    Task<Teacher?> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Teacher>> GetAllAsync();
    Task<Teacher> AddAsync(Teacher teacher);
    Task UpdateAsync(Teacher teacher);
    Task DeleteAsync(Guid id);
}
