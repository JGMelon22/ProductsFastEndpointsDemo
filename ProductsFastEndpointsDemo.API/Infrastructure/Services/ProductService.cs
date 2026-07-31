using ProductsFastEndpointsDemo.Exceptions;
using ProductsFastEndpointsDemo.Infrastructure.Interfaces;
using ProductsFastEndpointsDemo.Products.DTOs;
using ProductsFastEndpointsDemo.Products.Mappings;
using ProductsFastEndpointsDemo.Shared;
using ZiggyCreatures.Caching.Fusion;

namespace ProductsFastEndpointsDemo.Infrastructure.Services;

public class ProductService(IProductRepository productRepository, IFusionCache cache) : IProductService
{
    public async Task<ProductResponse> AddAsync(ProductRequest request)
    {
        if (Availability(request))
            throw new ProductAvailabilityException(request.Quantity, request.IsAvailable);

        var product = await productRepository.AddAsync(request.ToDomain());

        await cache.SetAsync($"product:{product.Id}", product);

        return product.ToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        await productRepository.DeleteAsync(id);

        await cache.RemoveAsync(id.ToString());
    }

    public async Task<PagedResponseOffset<ProductResponse>> GetAllPaginatedAsync(
        int pageNumber,
        int pageSize
    )
    {
        var cacheKey = $"products:page{pageNumber}:size{pageSize}";

        var pagedProducts =  await cache.GetOrSetAsync(cacheKey,
            _ => productRepository.GetAllAsync(pageNumber, pageSize));

        return pagedProducts.ToResponse();
    }

    public async Task<ProductResponse?> GetByIdAsync(Guid id)
    {
        var product = await cache.GetOrSetAsync(
            $"product:{id}",
            _ => productRepository.GetByIdAsync(id)
        );

        if (product is null)
            return null;

        return product.ToResponse();
    }

    public async Task<ProductResponse?> UpdateAsync(Guid id, ProductRequest request)
    {
        if (Availability(request))
            throw new ProductAvailabilityException(request.Quantity, request.IsAvailable);

        var product = await productRepository.UpdateAsync(id, request.ToDomain());

        if (product is null)
            return null;

        await cache.SetAsync($"product:{product.Id}", product);

        return product.ToResponse();
    }

    private static bool Availability(ProductRequest request)
        => (request.Quantity == 0 && request.IsAvailable) || (request.Quantity > 0 && !request.IsAvailable);
}
