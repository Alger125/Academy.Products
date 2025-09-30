using Academy.Products.Domain.Common.EntitiesBase;
using Academy.Products.Domain.Entities.ProductEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Academy.Products.Infrastructure.Persistence.Configuration.ModelBuilderExtensions;

namespace Academy.Products.Infrastructure.Persistence.Configuration;

public class ProductConfiguration : EntityTypeConfiguration<Product>
{
    public override void Map(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.description)
            .HasMaxLength(500);

        builder.Property(e => e.price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.stock)
            .IsRequired();
    }
}
