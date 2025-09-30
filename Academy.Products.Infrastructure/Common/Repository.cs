using Academy.Products.Domain.Common.EntitiesBase;
using Academy.Products.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Academy.Products.Infrastructure.Common;

public abstract class Repository<TEntity, TEntityId>(ApplicationDbContextObjects dbContext)
    where TEntity : Entity <TEntityId>
    where TEntityId : class
{
    protected readonly ApplicationDbContextObjects DbContext = dbContext;

    /*

    public virtual Task<TEntity?> GetByIdAsync(TEntity id)
    {
        return DbContext.Set<TEntity>().SingleOrDefaultAsync(p => p.Id == id);
    }

    public virtual IQueryable<TEntity> GetAllAsync()
    {
        return DbContext.Set<TEntity>().AsQueryable();
    }

    public void Add(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return DbContext.SaveChangesAsync(cancellationToken);
    }
    */
}
