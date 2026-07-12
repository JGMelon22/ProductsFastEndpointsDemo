using FastEndpoints;
using ProductsFastEndpointsDemo.Infrastructure.Interfaces;
using ProductsFastEndpointsDemo.Products.DTOs;
using ProductsFastEndpointsDemo.Shared;

namespace ProductsFastEndpointsDemo.Endpoints;

public class GetAllProductsEndpoint(IProductService productService)
    : EndpointWithoutRequest<PagedResponseOffset<ProductResponse>>
{
    public override void Configure()
    {
        Get("api/product/list");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        int pageSize = Query<int>("pageSize", isRequired: false);
        int pageNumber = Query<int>("pageNumber", isRequired: false);

        var products = await productService.GetAllPaginatedAsync(pageNumber, pageSize);

        if (!products.Data.Any())
        {
            await Send.NoContentAsync(cancellation: ct);
            return;
        }

        await Send.OkAsync(products, cancellation: ct);
    }
}