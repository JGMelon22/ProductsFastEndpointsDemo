using FastEndpoints;
using ProductsFastEndpointsDemo.Infrastructure.Interfaces;
using ProductsFastEndpointsDemo.Products.DTOs;
using ProductsFastEndpointsDemo.Shared;

namespace ProductsFastEndpointsDemo.Endpoints;

public class UpdateProductEndpoint(IProductService productService)
    : Endpoint<ProductRequest, ProductResponse>
{
    public override void Configure()
    {
        Patch("api/product/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ProductRequest req, CancellationToken ct)
    {
        Guid id = Route<Guid>("id");

        var result = await productService.UpdateAsync(id, req);

        await (result switch
        {
            ProductResponse response => Send.OkAsync(response, cancellation: ct),
            Error { Code: ErrorCodes.InvalidProductQuantity } => Send.ErrorsAsync(400, cancellation: ct),
            Error { Code: ErrorCodes.ProductNotFound } => Send.ErrorsAsync(404, cancellation: ct),
            _ => throw new ArgumentOutOfRangeException()
        });
    }
}