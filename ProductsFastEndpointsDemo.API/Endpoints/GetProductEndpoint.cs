using FastEndpoints;
using ProductsFastEndpointsDemo.Infrastructure.Interfaces;
using ProductsFastEndpointsDemo.Products.DTOs;

namespace ProductsFastEndpointsDemo.Endpoints
{
    public class GetProductEndpoint(IProductService productService)
        : EndpointWithoutRequest<ProductResponse>
    {
        public override void Configure()
        {
            Get("api/product/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            Guid id = Route<Guid>("id");

            var product = await productService.GetByIdAsync(id);

            if (product is null)
            {
                await Send.NotFoundAsync(cancellation: ct);
                return;
            }

            await Send.OkAsync(product, cancellation: ct);
        }
    }
}