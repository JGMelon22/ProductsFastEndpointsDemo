using ProductsFastEndpointsDemo.Products.DTOs;
using ProductsFastEndpointsDemo.Shared;

namespace ProductsFastEndpointsDemo.Infrastructure.Interfaces;

public interface IProductService
{
    Task<PagedResponseOffset<ProductResponse>> GetAllPaginatedAsync(int pageNumber, int pageSize);
    Task<ProductResponse?> GetByIdAsync(Guid id);
    Task<ProductResponse?> UpdateAsync(Guid id, ProductRequest request);
    Task DeleteAsync(Guid id);
    Task<ProductResponse> AddAsync(ProductRequest request);
}
