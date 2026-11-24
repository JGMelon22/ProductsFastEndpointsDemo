using ProductsFastEndpointsDemo.Products.DTOs;
using ProductsFastEndpointsDemo.Shared;

namespace ProductsFastEndpointsDemo.Infrastructure.Interfaces;

public interface IProductService
{
    Task<PagedResponseOffset<ProductResponse>> GetAllPaginatedAsync(int pageNumber, int pageSize);

    Task<PagedResponseOffset<ProductResponse>> GetAllPaginatedRefinedAsync(string searchTerm, string sortBy, bool ascending,
        int pageNumber, int pageSize);

    Task<Result<ProductResponse>> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductResponse?>> GetByName(string name);
    Task<Result<ProductResponse>> UpdateAsync(Guid id, ProductRequest request);
    Task DeleteAsync(Guid id);
    Task<Result<ProductResponse>> AddAsync(ProductRequest request);
}