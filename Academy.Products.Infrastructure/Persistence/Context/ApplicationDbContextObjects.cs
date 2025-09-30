
using Academy.Products.Domain;
using Academy.Products.Domain.Entities.ProductEntity;
using Academy.Products.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Academy.Products.Infrastructure.Context;

public class ApplicationDbContextObjects
{
    public virtual DbSet<Product> Products { get; set; }

    internal object Set<T>()
    {
    throw new NotImplementedException();
    }

/*
    public static DbSet<Product> Products(this ApplicationDbContext context)
    {
        return context.Set<Product>();
    }
*/
        
}
