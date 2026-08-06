using FastEndpoints;
using ProductsFastEndpointsDemo.Infrastructure.Interfaces;
using ProductsFastEndpointsDemo.Products.DTOs;
using ProductsFastEndpointsDemo.Shared;

namespace ProductsFastEndpointsDemo.Endpoints;

public class GetAllProductsRefined(IProductService productService)
    : EndpointWithoutRequest<PagedResponseOffset<ProductResponse>>
{
    public override void Configure()
    {
        Get("api/product/list/refined");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        string searchTerm = Query<string>("searchTerm", isRequired: false) ?? "";
        string sortBy = Query<string>("sortBy", isRequired: false) ?? "name";
        bool ascending = Query<bool?>("ascending", isRequired: false) ?? true;
        int pageNumber = Query<int?>("pageNumber", isRequired: false) ?? 0;
        int pageSize = Query<int?>("pageSize", isRequired: false) ?? 10;

        var products = await productService.GetAllPaginatedRefinedAsync(searchTerm, sortBy, ascending, pageNumber, pageSize);

        if (!products.Data.Any())
        {
            await Send.NoContentAsync(cancellation: ct);
            return;
        }

        await Send.OkAsync(products, cancellation: ct);
    }
}