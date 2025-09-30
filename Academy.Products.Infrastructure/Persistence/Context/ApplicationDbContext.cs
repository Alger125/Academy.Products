using Academy.Products.Domain.Entities.ProductEntity;
using Microsoft.EntityFrameworkCore;
using Academy.Products.Infrastructure.Persistence.Configuration;

namespace Academy.Products.Infrastructure.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    private string _connectionString = "ConnectionString";

    // constructor vacío
    public ApplicationDbContext()
    {

    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    public ApplicationDbContext(ConnectionStrings connectionStrings)
    {
        _connectionString = connectionStrings.DefaultConnection;
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddConfiguration(new ProductConfiguration());
    }
    

}

/*
public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<DbContextOptionsCustom> databaseOptions) : DbContext(options)
{
    private readonly IOptions<DbContextOptionsCustom> _databaseOptions = databaseOptions;
}
*/


/*
public partial class ApplicationDbContext : DbContext
{
    private readonly IOptions<DbContextOptionsCustom> _databaseOptions;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
     IOptions<DbContextOptionsCustom> databaseOptions)
      : base(options)
    {
        _databaseOptions = databaseOptions;
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.description).IsRequired().HasMaxLength(500);
            entity.Property(e => e.price).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.stock).IsRequired();
            entity.Property(e => e.category).IsRequired().HasMaxLength(50);
            entity.Property(e => e.imageURL).HasMaxLength(200);
        });


    }
}
*/
