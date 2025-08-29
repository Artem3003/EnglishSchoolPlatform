using Domain.Entities;

namespace Domain.Interfaces;

public interface IAdminRepository
{
    Task<Admin?> GetByIdAsync(Guid id);
    Task<Admin?> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Admin>> GetAllAsync();
    Task<Admin> AddAsync(Admin admin);
    Task UpdateAsync(Admin admin);
    Task DeleteAsync(Guid id);
}
