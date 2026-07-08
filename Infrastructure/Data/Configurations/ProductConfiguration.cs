using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsFastEndpointsDemo.Products.Entities;

namespace ProductsFastEndpointsDemo.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(p => p.Id);

        builder.HasIndex(p => p.Id)
            .HasDatabaseName("idx_product_id");

        builder.Property(p => p.Id)
            .HasColumnName("id");

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("name")
            .HasColumnType("VARCHAR");

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnName("price")
            .HasPrecision(6, 2) // 9999.99
            .HasColumnType("DECIMAL");

        builder.Property(p => p.Quantity)
                    .IsRequired()
                    .HasColumnName("quantity")
                    .HasColumnType("INT");

        builder.Property(p => p.IsAvailable)
            .IsRequired()
            .HasColumnName("is_available")
            .HasColumnType("BOOLEAN");
    }
}