using Academy.Products.Domain.Common.EntitiesBase;
using Academy.Products.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Academy.Products.Infrastructure.Common;

public abstract class Repository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : class
{
    protected readonly ApplicationDbContext Dbcontext;

    protected Repository(ApplicationDbContext dbcontext)
    {
        Dbcontext = dbcontext;
    }

    public virtual async Task<TEntity> GetByIdAsync(TEntityId id)
    {
        return await Dbcontext.Set<TEntity>().FindAsync(id);
    }

    public virtual IQueryable<TEntity> GetAll()
    {
        return Dbcontext.Set<TEntity>().AsQueryable();
    }

    public virtual void Add(TEntity entity)
    {
        Dbcontext.Set<TEntity>().Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
        Dbcontext.Set<TEntity>().Update(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        Dbcontext.Set<TEntity>().Remove(entity);
    }
    
    public virtual async Task<int> SaveChangesAsync( CancellationToken cancellationToken = default)
    {
        return await Dbcontext.SaveChangesAsync(cancellationToken);
    }
}
