using Domain.Entities;
using Domain.Interfaces;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories;

public class AdminRepository(ApplicationDbContext context) : IAdminRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Admin?> GetByIdAsync(Guid id)
    {
        return await _context.Admins.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Admin?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Admins.Include(a => a.User).FirstOrDefaultAsync(a => a.UserId == userId);
    }

    public async Task<IEnumerable<Admin>> GetAllAsync()
    {
        return await _context.Admins.Include(a => a.User).ToListAsync();
    }

    public async Task<Admin> AddAsync(Admin admin)
    {
        _context.Admins.Add(admin);
        await _context.SaveChangesAsync();
        return admin;
    }

    public async Task UpdateAsync(Admin admin)
    {
        _context.Admins.Update(admin);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var admin = await _context.Admins.FindAsync(id);
        if (admin != null)
        {
            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
        }
    }
}
