using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Ventiontask.Data.DbContexts;
using Ventiontask.Data.IRepositories;
using Ventiontask.Domain.Commons;

namespace Ventiontask.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    protected readonly AppDbContext dbContext;
    protected readonly DbSet<TEntity> dbSet;

    public Repository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = dbContext.Set<TEntity>();
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await this.SelectAsync(expression);
        if(entity != null)
        {
            dbSet.Remove(entity);
            return true;
        }

        return false;
    }

    public async ValueTask<TEntity> InsertAsync(TEntity entity)
    {
        var entry = await dbSet.AddAsync(entity);
        return entry.Entity;
    }

    public async ValueTask SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression)
        => expression is null ? dbSet : dbSet.Where(expression);


    public async ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression)
        => await this.SelectAll(expression).FirstOrDefaultAsync();

    public TEntity Update(TEntity entity)
    {
        var entry = dbSet.Update(entity);
        return entry.Entity;
    }
}
