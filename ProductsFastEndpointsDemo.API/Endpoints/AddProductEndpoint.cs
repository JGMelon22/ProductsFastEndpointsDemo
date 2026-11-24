using FastEndpoints;
using ProductsFastEndpointsDemo.Infrastructure.Interfaces;
using ProductsFastEndpointsDemo.Products.DTOs;
using ProductsFastEndpointsDemo.Shared;

namespace ProductsFastEndpointsDemo.Endpoints;

public class AddProductEndpoint(IProductService productService) : Endpoint<ProductRequest, ProductResponse>
{
    public override void Configure()
    {
        Post("api/product/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ProductRequest req, CancellationToken ct)
    {
        var result = await productService.AddAsync(req);

        await (result switch
        {
            ProductResponse response => Send.CreatedAtAsync<GetProductEndpoint>(new { id = response.Id }, response,
                cancellation: ct),
            Error { Code: ErrorCodes.InvalidProductQuantity } => Send.ErrorsAsync(400, cancellation: ct),
            _ => throw new ArgumentOutOfRangeException()
        });
    }
}