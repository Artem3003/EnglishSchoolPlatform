using Domain.Entities;
using Domain.Interfaces;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories;

public class StudentRepository(ApplicationDbContext context) : IStudentRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Student?> GetByIdAsync(Guid id)
    {
        return await _context.Students.Include(s => s.User).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Student?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Students.Include(s => s.User).FirstOrDefaultAsync(s => s.UserId == userId);
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _context.Students.Include(s => s.User).ToListAsync();
    }

    public async Task<Student> AddAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task UpdateAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}
