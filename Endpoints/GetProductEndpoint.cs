using FastEndpoints;
using ProductsFastEndpointsDemo.Infrastructure.Interfaces;
using ProductsFastEndpointsDemo.Products.DTOs;

namespace ProductsFastEndpointsDemo.Endpoints
{
    public class GetProductEndpoint(IProductService productService)
        : Endpoint<GetProductRequest, ProductResponse>
    {
        public override void Configure()
        {
            Get("api/product/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetProductRequest req, CancellationToken ct)
        {
            //Guid id = Route<Guid>("id");

            var product = await productService.GetByIdAsync(req.Id);

            if (product is null)
            {
                await Send.NotFoundAsync();
                return;
            }

            await Send.OkAsync(product);
        }
    }
}

public class GetProductRequest
{
    [BindFrom("id")]
    public Guid Id { get; set; }
}
