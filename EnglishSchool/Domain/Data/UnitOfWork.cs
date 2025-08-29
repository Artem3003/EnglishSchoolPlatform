using Domain.Repositories;
using Domain.Interfaces;

namespace Domain.Data;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;
    private IUserRepository? _users;
    private ITeacherRepository? _teachers;
    private IStudentRepository? _students;
    private IAdminRepository? _admins;

    public IUserRepository Users => _users ??= new UserRepository(_context);
    public ITeacherRepository Teachers => _teachers ??= new TeacherRepository(_context);
    public IStudentRepository Students => _students ??= new StudentRepository(_context);
    public IAdminRepository Admins => _admins ??= new AdminRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_context.Database.CurrentTransaction != null)
        {
            await _context.Database.CurrentTransaction.CommitAsync();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_context.Database.CurrentTransaction != null)
        {
            await _context.Database.CurrentTransaction.RollbackAsync();
        }
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
