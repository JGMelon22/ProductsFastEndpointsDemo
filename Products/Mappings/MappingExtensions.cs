using ProductsFastEndpointsDemo.Products.DTOs;
using ProductsFastEndpointsDemo.Products.Entities;

namespace ProductsFastEndpointsDemo.Products.Mappings;

public static class MappingExtensions
{
    public static Product ToDomain(this ProductRequest request)
        => new(request.Name, request.Price, request.Quantity, request.IsAvailable);

    public static ProductResponse ToResponse(this Product response)
        => new()
        {
            Id = response.Id,
            Name = response.Name,
            Price = response.Price,
            Quantity = response.Quantity,
            IsAvailable = response.IsAvailable
        };

    public static IEnumerable<ProductResponse> ToResponse(IEnumerable<Product> products)
        => products.Select(p => p.ToResponse());
}