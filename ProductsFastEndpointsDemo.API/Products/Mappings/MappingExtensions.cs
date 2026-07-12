using ProductsFastEndpointsDemo.Products.DTOs;
using ProductsFastEndpointsDemo.Products.Entities;
using ProductsFastEndpointsDemo.Shared;

namespace ProductsFastEndpointsDemo.Products.Mappings;

public static class MappingExtensions
{
    public static Product ToDomain(this ProductRequest request) =>
        new(request.Name, request.Price, request.Quantity, request.IsAvailable);

    public static ProductResponse ToResponse(this Product product) =>
        new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            IsAvailable = product.IsAvailable,
        };

    public static IEnumerable<ProductResponse?> ToResponse(this IEnumerable<Product?> products) =>
        products.Select(p => p.ToResponse());

    public static PagedResponseOffset<ProductResponse> ToResponse(
        this PagedResponseOffset<Product> paged
    ) =>
        new(
            paged.Data.Select(p => p.ToResponse()).ToList(),
            paged.PageNumber,
            paged.PageSize,
            paged.TotalRecords
        );
}
