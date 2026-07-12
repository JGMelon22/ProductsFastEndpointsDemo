using Microsoft.EntityFrameworkCore;
using ProductsFastEndpointsDemo.Infrastructure.Data.Configurations;
using ProductsFastEndpointsDemo.Products.Entities;

namespace ProductsFastEndpointsDemo.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }
}
