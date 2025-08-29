using Domain.Entities;
using Domain.Interfaces;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories;

public class TeacherRepository(ApplicationDbContext context) : ITeacherRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Teacher?> GetByIdAsync(Guid id)
    {
        return await _context.Teachers.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Teacher?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Teachers.Include(t => t.User).FirstOrDefaultAsync(t => t.UserId == userId);
    }

    public async Task<IEnumerable<Teacher>> GetAllAsync()
    {
        return await _context.Teachers.Include(t => t.User).ToListAsync();
    }

    public async Task<Teacher> AddAsync(Teacher teacher)
    {
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();
        return teacher;
    }

    public async Task UpdateAsync(Teacher teacher)
    {
        _context.Teachers.Update(teacher);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher != null)
        {
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
        }
    }
}
