namespace ProductsFastEndpointsDemo.Shared;

public sealed record Error(string Code, string Description);

public static class ErrorCodes
{
    public const string InvalidProductQuantity = "Products.InvalidQuantity";
    public const string ProductNotFound = "Products.NotFound";
}

public static class ProductErrors
{
    public static readonly Error InvalidQuantity = new(ErrorCodes.InvalidProductQuantity, "Invalid product quantity. Check stock first");
    public static readonly Error NotFound = new(ErrorCodes.ProductNotFound, "Product with specified Id not found");
}