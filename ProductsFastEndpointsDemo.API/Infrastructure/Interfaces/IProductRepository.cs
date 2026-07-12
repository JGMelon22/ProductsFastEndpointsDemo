using ProductsFastEndpointsDemo.Products.Entities;
using ProductsFastEndpointsDemo.Shared;

namespace ProductsFastEndpointsDemo.Infrastructure.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<IEnumerable<Product?>> GetByNameAsync(string name);
    Task<PagedResponseOffset<Product>> GetAllAsync(int pageNumber, int pageSize);
    Task<PagedResponseOffset<Product>> GetAllPaginatedRefinedAsync(string searchTerm, string sortBy, bool ascending, int pageNumber, int pageSize);
    Task<Product> AddAsync(Product product);
    Task<Product?> UpdateAsync(Guid id, Product product);
    Task DeleteAsync(Guid id);
}
