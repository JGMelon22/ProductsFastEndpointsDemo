using ProductsFastEndpointsDemo.Products.DTOs;
using ProductsFastEndpointsDemo.Shared;

namespace ProductsFastEndpointsDemo.Infrastructure.Interfaces;

public interface IProductService
{
    Task<PagedResponseOffset<ProductResponse>> GetAllPaginatedAsync(int pageNumber, int pageSize);
    Task<ProductResponse?> GetByIdAsync(int id);
    Task<ProductResponse?> UpdateAsync(int id, ProductRequest request);
    Task DeleteAsync(int id);
    Task<ProductResponse> AddAsync(ProductRequest request);
}