using FastEndpoints;
using ProductsFastEndpointsDemo.Infrastructure.Interfaces;
using ProductsFastEndpointsDemo.Products.DTOs;
using ProductsFastEndpointsDemo.Shared;

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

            var result = await productService.GetByIdAsync(id);

            await (result switch
            {
                ProductResponse response => Send.OkAsync(response, cancellation: ct),
                Error { Code: ErrorCodes.ProductNotFound } => Send.NotFoundAsync(cancellation: ct),
                _ => throw new ArgumentOutOfRangeException()
            });
        }
    }
}