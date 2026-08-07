using FastEndpoints;
using ProductsFastEndpointsDemo.Infrastructure.Interfaces;
using ProductsFastEndpointsDemo.Products.DTOs;

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

            var product = await productService.UpdateAsync(id, req);
            
            if (product is null)
            {
                await Send.ErrorsAsync(400, cancellation: ct);
                return;
            }

            await Send.OkAsync(product, cancellation: ct);
        }
}