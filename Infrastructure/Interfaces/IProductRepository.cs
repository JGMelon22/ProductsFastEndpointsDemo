using ProductsFastEndpointsDemo.Products.Entities;
using ProductsFastEndpointsDemo.Shared;

namespace ProductsFastEndpointsDemo.Infrastructure.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<PagedResponseOffset<Product>> GetAllAsync(int pageNumber, int pageSize);
    Task<Product> AddAsync(Product product);
    Task<Product?> UpdateAsync(int id, Product product);
    Task DeleteAsync(int id);
}