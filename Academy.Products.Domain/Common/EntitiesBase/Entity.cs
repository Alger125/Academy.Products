namespace Academy.Products.Domain.Common.EntitiesBase;

public abstract class Entity<TEntity>
{
    // ¿Cambiar por productId?
    public TEntity Id { get; set; }

    protected Entity(TEntity id)
    {
        Id = id;
    }

#pragma warning disable
    protected Entity()
    {

    }
#pragma warning restore

}
