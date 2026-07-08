namespace ProductsFastEndpointsDemo.Products.DTOs;

public record ProductResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public bool IsAvailable { get; init; }
}