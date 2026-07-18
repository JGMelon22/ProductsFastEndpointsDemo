using FastEndpoints;
using ProductsFastEndpointsDemo.Infrastructure.Interfaces;
using ProductsFastEndpointsDemo.Products.DTOs;

namespace ProductsFastEndpointsDemo.Endpoints;

public class AddProductEndpoint(IProductService productService) : Endpoint<ProductRequest, ProductResponse>
{
    public override void Configure()
    {
        Post("api/product/create");
        AllowAnonymous();
        Idempotency();
    }

    public override async Task HandleAsync(ProductRequest req, CancellationToken ct)
    {
        var product = await productService.AddAsync(req);
        await Send.CreatedAtAsync<GetProductEndpoint>(
            new { id = product.Id },
            product,
            cancellation: ct
        );
    }
}