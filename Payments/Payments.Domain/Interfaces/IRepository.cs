using System.Linq.Expressions;
using Payments.Domain.Entities.Common;

namespace Payments.Domain.Interfaces;

public interface IRepository<TEntity>
    where TEntity : BaseEntity<Guid>
{
    Task<TEntity> AddAsync(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    Task<TEntity?> GetByIdAsync(Guid id);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
}
