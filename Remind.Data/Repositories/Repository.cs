using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Remind.Core.Repositories;

namespace Remind.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbContext Context;

    public Repository(DbContext context)
    {
        this.Context = context;
    }

    public async ValueTask<TEntity> GetByIdAsync(Guid id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
    {
        return Context.Set<TEntity>().Where(expression);
    }

    public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
    {
        return Context.Set<TEntity>().SingleOrDefaultAsync(expression);
    }

    public async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await Context.Set<TEntity>().AddRangeAsync();
    }

    public void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().RemoveRange(entities);
    }
}