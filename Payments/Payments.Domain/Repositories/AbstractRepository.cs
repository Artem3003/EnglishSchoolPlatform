using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Payments.Domain.Data;
using Payments.Domain.Entities.Common;
using Payments.Domain.Interfaces;

namespace Payments.Domain.Repositories;

public abstract class AbstractRepository<TEntity>(PaymentsDbContext context) : IRepository<TEntity>
    where TEntity : BaseEntity<Guid>
{
    private readonly PaymentsDbContext _context = context;

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entry = await _context.AddAsync(entity);
        return entry.Entity;
    }

    public void Delete(TEntity entity)
    {
        _context.Remove(entity);
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        var entity = await _context.FindAsync<TEntity>(id);
        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public void Update(TEntity entity)
    {
        _context.Update(entity);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().Where(predicate).ToListAsync();
    }
}
