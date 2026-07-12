using ProductsFastEndpointsDemo.Exceptions;
using ProductsFastEndpointsDemo.Infrastructure.Interfaces;
using ProductsFastEndpointsDemo.Products.DTOs;
using ProductsFastEndpointsDemo.Products.Entities;
using ProductsFastEndpointsDemo.Products.Mappings;
using ProductsFastEndpointsDemo.Shared;

namespace ProductsFastEndpointsDemo.Infrastructure.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<ProductResponse> AddAsync(ProductRequest request)
    {
        if (Availability(request))
            throw new ProductAvailabilityException(request.Quantity, request.IsAvailable);

        var product = await productRepository.AddAsync(request.ToDomain());

        return product.ToResponse();
    }


    public async Task DeleteAsync(Guid id)
    {
        await productRepository.DeleteAsync(id);
    }

    public async Task<PagedResponseOffset<ProductResponse>> GetAllPaginatedAsync(
        int pageNumber,
        int pageSize
    )
    {
        var pagedProducts = await productRepository.GetAllAsync(pageNumber, pageSize);

        return pagedProducts.ToResponse();
    }

    public async Task<PagedResponseOffset<ProductResponse>> GetAllPaginatedRefinedAsync(string searchTerm, string sortBy, bool ascending, int pageNumber, int pageSize )
    {
        var pagedProducts = await productRepository.GetAllAsync(searchTerm, sortBy, ascending, pageNumber, pageSize);

        return pagedProducts.ToResponse();
    }

    public async Task<ProductResponse?> GetByIdAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
            return null;

        return product.ToResponse();
    }

    public async Task<IEnumerable<ProductResponse?>> GetByName(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            return Enumerable.Empty<ProductResponse>();
        
        IEnumerable<Product?> products = await productRepository.GetByNameAsync(name);

        return products.ToResponse();
    }

    public async Task<ProductResponse?> UpdateAsync(Guid id, ProductRequest request)
    {
        if (Availability(request))
            throw new ProductAvailabilityException(request.Quantity, request.IsAvailable);

        var product = await productRepository.UpdateAsync(id, request.ToDomain());

        if (product is null)
            return null;

        return product.ToResponse();
    }

    private static bool Availability(ProductRequest request)
        => (request.Quantity == 0 && request.IsAvailable) || (request.Quantity > 0 && !request.IsAvailable);
}