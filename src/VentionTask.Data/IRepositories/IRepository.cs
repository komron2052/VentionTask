using System.Linq.Expressions;
using Ventiontask.Domain.Commons;

namespace Ventiontask.Data.IRepositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    ValueTask<TEntity> InsertAsync(TEntity entity);
    TEntity Update(TEntity entity);
    ValueTask<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
    ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression);
    IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null);
    ValueTask SaveAsync();
}
