using FastEndpoints;
using ProductsFastEndpointsDemo.Infrastructure.Interfaces;
using ProductsFastEndpointsDemo.Products.DTOs;

namespace ProductsFastEndpointsDemo.Endpoints;

public class GetProductByNameEndpoint(IProductService productService)
    : EndpointWithoutRequest<IEnumerable<ProductResponse?>>
{
    public override void Configure()
    {
        Get("api/product/names/{name}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        string name = Route<string>("name", isRequired: true)!;

        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
        {
            await Send.ErrorsAsync(400, cancellation: ct);
            return;
        }

        var product = productService.GetByName(name);

        await Send.OkAsync(product.Result, cancellation: ct);
    }
}