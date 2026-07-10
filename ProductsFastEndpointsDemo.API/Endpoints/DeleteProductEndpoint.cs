using FastEndpoints;
using ProductsFastEndpointsDemo.Infrastructure.Interfaces;

namespace ProductsFastEndpointsDemo.Endpoints;

public class DeleteProductEndpoint(IProductService productService) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("api/product/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Guid id = Route<Guid>("id");

        await productService.DeleteAsync(id);
        await Send.NoContentAsync(cancellation: ct);
    }
}