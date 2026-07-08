using ProductsFastEndpointsDemo.Products.Entities;
using ProductsFastEndpointsDemo.Shared;

namespace ProductsFastEndpointsDemo.Infrastructure.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync();
    Task<PagedResponseOffset<Product>> GetAllAsync(int pageNumber, int pageSize);
    Task<Product> AddAsync(Product product);
    void UpdateAsync(int id, Product product);
    void DeleteAsync(int id);
}