using ProductsFastEndpointsDemo.Infrastructure.Interfaces;
using ProductsFastEndpointsDemo.Infrastructure.Repositories;
using ProductsFastEndpointsDemo.Products.DTOs;
using ProductsFastEndpointsDemo.Products.Entities;
using ProductsFastEndpointsDemo.Products.Mappings;
using ProductsFastEndpointsDemo.Shared;

namespace ProductsFastEndpointsDemo.Infrastructure.Services;

public class ProductService(ProductRepository productRepository) : IProductService
{
    public async Task<ProductResponse> AddAsync(ProductRequest request)
    {
        if (request.Quantity == 0 && request.IsAvailable == true)
            throw new Exception("Product quantity can not be 0 if it is available in stock.");

        var product = await productRepository.AddAsync(request.ToDomain());

        return product.ToResponse();
    }

    public async Task DeleteAsync(int id)
    {
        await productRepository.DeleteAsync(id);
    }

    public async Task<PagedResponseOffset<ProductResponse>> GetAllPaginatedAsync(int pageNumber, int pageSize)
    {
        var pagedProducts = await productRepository.GetAllAsync(pageNumber, pageSize);

        return pagedProducts.ToResponse();
    }

    public async Task<ProductResponse?> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
            return null;

        return product.ToResponse();
    }

    public async Task<ProductResponse?> UpdateAsync(int id, ProductRequest request)
    {
        if (request.Quantity == 0 && request.IsAvailable == true)
            throw new Exception("Product quantity can not be 0 if it is available in stock.");

        var product = await productRepository.UpdateAsync(id, request.ToDomain());

        if (product is null)
            return null;

        return product.ToResponse();
    }
}