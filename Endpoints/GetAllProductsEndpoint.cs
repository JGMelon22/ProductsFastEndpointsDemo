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

        if (pageNumber < 1)
            pageNumber = 0;
        if (pageSize < 1)
            pageSize = 10;

        var products = await productService.GetAllPaginatedAsync(pageNumber, pageSize);

        if (!products.Data.Any())
        {
            await Send.NoContentAsync();
            return;
        }

        await Send.OkAsync(products);
    }
}
