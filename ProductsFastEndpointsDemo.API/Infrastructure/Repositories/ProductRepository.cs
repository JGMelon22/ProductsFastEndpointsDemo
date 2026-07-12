using Microsoft.EntityFrameworkCore;
using ProductsFastEndpointsDemo.Infrastructure.Data;
using ProductsFastEndpointsDemo.Infrastructure.Interfaces;
using ProductsFastEndpointsDemo.Products.Entities;
using ProductsFastEndpointsDemo.Shared;

namespace ProductsFastEndpointsDemo.Infrastructure.Repositories;

public class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public async Task<PagedResponseOffset<Product>> GetAllPaginatedRefinedAsync(string searchTerm, string sortBy,
        bool ascending, int pageNumber, int pageSize)
    {
        int totalRecords = await dbContext.Products.AsNoTracking().CountAsync();
        IQueryable<Product> query = dbContext.Products;

        if (!string.IsNullOrEmpty(searchTerm))
        {
            string pattern = $"%{searchTerm}%";
            query = query
                .Where(p => EF.Functions.Like(p.Name, pattern));
        }

        query = sortBy.ToLowerInvariant() switch
        {
            "price" => ascending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price),
            _ => ascending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name)
        };

        var products = await query
            .AsNoTracking()
            .Skip((pageNumber - 1) / pageSize)
            .Take(pageSize)
            .ToListAsync();

        PagedResponseOffset<Product> pagedResponse = new(
            products,
            pageNumber,
            pageSize,
            totalRecords
        );

        return pagedResponse;
    }

    public async Task<Product> AddAsync(Product product)
    {
        await dbContext.Products.AddAsync(product);
        await dbContext.SaveChangesAsync();

        return product;
    }

    public async Task DeleteAsync(Guid id)
    {
        Product? product = await dbContext.Products.FindAsync(id);

        if (product is not null)
        {
            dbContext.Products.Remove(product);

            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Product?>> GetByNameAsync(string name)
    {
        string pattern = $"{name}%";

        return await dbContext.Products
            .AsNoTracking()
            .Where(p => EF.Functions.Like(p.Name, pattern))
            .OrderBy(p => p.Name)
            .Take(10)
            .ToListAsync();
    }

    public async Task<PagedResponseOffset<Product>> GetAllAsync(int pageNumber, int pageSize)
    {
        int totalRecords = await dbContext.Products.AsNoTracking().CountAsync();

        List<Product> products = await dbContext
            .Products.AsNoTracking()
            .OrderBy(p => p.Name)
            .Skip((pageNumber - 1) / pageSize)
            .Take(pageSize)
            .ToListAsync();

        PagedResponseOffset<Product> pagedResponse = new(
            products,
            pageNumber,
            pageSize,
            totalRecords
        );

        return pagedResponse;
    }

    public async Task<Product?> GetByIdAsync(Guid id) => await dbContext.Products.FindAsync(id);

    public async Task<Product?> UpdateAsync(Guid id, Product product)
    {
        Product? existingProduct = await dbContext.Products.FindAsync(id);

        if (existingProduct is null)
            return null;

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Quantity = product.Quantity;
        existingProduct.IsAvailable = product.IsAvailable;

        await dbContext.SaveChangesAsync();

        return existingProduct;
    }
}